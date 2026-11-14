using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Weardian.Client.Core.DTOs.Sync.Response.Get;
using Weardian.Client.Core.DTOs.Sync.Response.Post;
using Weardian.Client.Core.DTOs.Sync.Transfers;
using Weardian.Client.Core.Interfaces.APIClient.Sync;
using Weardian.Client.Core.Interfaces.Auth;
using Weardian.Client.Core.Interfaces.Sync;
using Weardian.Client.Infrastructure.Serialization;

namespace Weardian.Client.Infrastructure.Services.Sync
{
    public class EnvelopeTransferService : IEnvelopeTransferService
    {
        private readonly IEnvelopeAPIClient _envelopeAPIClient;
        private readonly IKeyRecordSyncService _keyRecordSyncService;
        private readonly IPayloadRecordSyncService _payloadRecordSyncService;

        public EnvelopeTransferService(

            IEnvelopeAPIClient envelopeAPIClient,
            IKeyRecordSyncService keyRecordSyncService,
            IPayloadRecordSyncService payloadRecordSyncService)
        {
            _envelopeAPIClient = envelopeAPIClient;
            _keyRecordSyncService = keyRecordSyncService;
            _payloadRecordSyncService = payloadRecordSyncService;
        }
        private async Task<IReadOnlyList<EncryptedEnvelopeSyncDto>> GetSymmetricLocalEnvelopesAsync()
        {
            var payloadRecords = await _payloadRecordSyncService.GetAllPayloadRecordsAsync();
            var keyRecords = await _keyRecordSyncService.GetAllKeyRecordsAsync();

            var envelopes = new List<EncryptedEnvelopeSyncDto>();

            foreach (var payload in payloadRecords)
            {
                var matchingRecord = keyRecords
                    .FirstOrDefault(key => key.EnvelopeId == payload.EnvelopeId);

                if (matchingRecord == null)
                    continue;

                var envelope = new EncryptedEnvelopeSyncDto(
                    EnvelopeId: payload.EnvelopeId,
                    KeyRequestDto: matchingRecord,
                    PayloadRequestDto: payload
                );
                envelopes.Add(envelope);
            }

            return envelopes;
        }

        public async Task SyncAllEnvelopesAsync()
        {
            var serverEnvelopes = await _envelopeAPIClient.GetSymmetricServerEnvelopesAsync();
            var localEnvelopes = await GetSymmetricLocalEnvelopesAsync();

           // upload local envelope to server if envelope does not exist on server
           foreach (var localEnvelope in localEnvelopes)
            {
                bool existsOnServer = serverEnvelopes
                    .Any(serverEnvelope => serverEnvelope.EnvelopeId == localEnvelope.EnvelopeId);

                if (!existsOnServer) 
                { 
                    await _envelopeAPIClient.SyncEncryptedEnvelopeAsync(localEnvelope);
                }
            }

           // downloads server envelope if local envelope does not exist and stores locally
           foreach (var serverEnvelope in serverEnvelopes)
            {
                bool existsLocal = localEnvelopes
                    .Any(localEnvelope => localEnvelope.EnvelopeId == serverEnvelope.EnvelopeId);

                if (!existsLocal)
                {
                    await _payloadRecordSyncService.AddPayloadRecordAsync(serverEnvelope.PayloadRecord!);
                    await _keyRecordSyncService.AddKeyRecordAsync(serverEnvelope.KeyRecord!);
                }
            }
        }
    }
}
