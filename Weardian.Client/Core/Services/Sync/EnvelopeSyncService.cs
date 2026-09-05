using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Weardian.Client.Core.DTOs.Sync.Response.Get;
using Weardian.Client.Core.DTOs.Sync.Response.Post;
using Weardian.Client.Core.DTOs.Sync.Transfers;
using Weardian.Client.Core.Interfaces.Auth;
using Weardian.Client.Core.Interfaces.Sync;

namespace Weardian.Client.Core.Services.Sync
{
    public class EnvelopeSyncService : IEnvelopeSyncService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthTokenStorage _authToken;
        private readonly IKeyRecordSyncService _keyRecordSyncService;
        private readonly IPayloadRecordSyncService _payloadRecordSyncService;

        public EnvelopeSyncService(
            HttpClient httpClient,
            IAuthTokenStorage authToken,
            IKeyRecordSyncService keyRecordSyncService,
            IPayloadRecordSyncService payloadRecordSyncService)
        {
            _httpClient = httpClient;
            _authToken = authToken;
            _keyRecordSyncService = keyRecordSyncService;
            _payloadRecordSyncService = payloadRecordSyncService;
        }

        public async Task<EncryptedEnvelopeSyncResponseDto> SyncEncryptedEnvelopeAsync(EncryptedEnvelopeSyncDto envelopeRequest)
        {

            var token = await _authToken.GetAccessTokenAsync();

            using var request = new HttpRequestMessage(HttpMethod.Post, "/api/keys/symmetric");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = JsonContent.Create(envelopeRequest);

            using var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var syncResponse = await response.Content.ReadFromJsonAsync<EncryptedEnvelopeSyncResponseDto>();

            if (syncResponse == null)
                throw new InvalidOperationException("Invalid envelope sync response");

            return syncResponse;
        }

        private async Task<IReadOnlyList<EncryptedEnvelopeResponseDto>> GetSymmetricServerEnvelopesAsync()
        {
            var token = await _authToken.GetAccessTokenAsync();

            using var request = new HttpRequestMessage(HttpMethod.Get, "/api/keys/symmetric");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var envelopes = await response.Content.ReadFromJsonAsync<List<EncryptedEnvelopeResponseDto>>();

            if (envelopes == null)
                throw new InvalidOperationException("Invalid envelope response from server");

            return envelopes;
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
                    KeyRecord: matchingRecord,
                    PayloadRecord: payload
                );

                envelopes.Add(envelope);
            }

            return envelopes;
        }

        public async Task SyncAllEnvelopesAsync()
        {
            var serverEnvelopes = await GetSymmetricServerEnvelopesAsync();
            var localEnvelopes = await GetSymmetricLocalEnvelopesAsync();

           // upload local envelope to server if envelope does not exist on server
           foreach (var localEnvelope in localEnvelopes)
            {
                var existsOnServer = serverEnvelopes
                    .Any(serverEnvelope => serverEnvelope.EnvelopeId == localEnvelope.EnvelopeId);

                if (!existsOnServer) 
                { 
                    await SyncEncryptedEnvelopeAsync(localEnvelope);
                }
            }

           // downloads server envelope if local envelope does not exist
           foreach (var serverEnvelope in serverEnvelopes)
            {
                var existsLocal = localEnvelopes
                    .Any(localEnvelope => localEnvelope.EnvelopeId == serverEnvelope.EnvelopeId);

                if (!existsLocal)
                {
                    
                }
            }
        }
    }
}
