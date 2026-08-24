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

        private async Task<IReadOnlyList<EncryptedEnvelopeResponseDto>> GetSymmetricLocalEnvelopesAsync()
        {
            var payloadRecords = await _payloadRecordSyncService.GetAllPayloadRecordsAsync();
            var keyRecords = await _keyRecordSyncService.GetAllKeyRecordsAsync();

            var envelopes = new List<EncryptedEnvelopeResponseDto>();

            foreach (var payload in payloadRecords)
            {
                var matchingRecord = keyRecords
                    .FirstOrDefault(key => key.EnvelopeId == payload.EnvelopeId);

                if (matchingRecord == null)
                    continue;

                var envelope = new EncryptedEnvelopeResponseDto(
                    EnvelopeId: payload.EnvelopeId,
                    KeyRecord: matchingRecord,
                    PayloadRecord: payload,
                    Success: true,
                    Error: null
                );

                envelopes.Add(envelope);
            }

            return envelopes;
        }

        public async Task SyncAllEnvelopesAsync()
        {
            var serverEnvelopes = await GetSymmetricServerEnvelopesAsync();
            var localEnvelopes = await GetSymmetricLocalEnvelopesAsync();
        }
    }
}
