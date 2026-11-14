using System.Net.Http;
using System.Net.Http.Json;
using Weardian.Client.Core.DTOs.Sync.Response.Get;
using Weardian.Client.Core.DTOs.Sync.Response.Post;
using Weardian.Client.Core.DTOs.Sync.Transfers;
using Weardian.Client.Core.Interfaces.APIClient.Sync;

namespace Weardian.Client.Infrastructure.Services.APIClient.Sync
{
    internal class EnvelopeAPIClient : IEnvelopeAPIClient
    {
        private readonly HttpClient _httpclient;

        public EnvelopeAPIClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<EncryptedEnvelopeSyncResponseDto> SyncEncryptedEnvelopeAsync(EncryptedEnvelopeSyncDto envelopeRequest)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "/api/keys/symmetric");
            request.Content = JsonContent.Create(envelopeRequest);

            using var response = await _httpclient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var syncResponse = await response.Content.ReadFromJsonAsync<EncryptedEnvelopeSyncResponseDto>();

            if (syncResponse == null)
                throw new InvalidOperationException("Invalid envelope sync response");

            return syncResponse;
        }

        public async Task<IReadOnlyList<EncryptedEnvelopeResponseDto>> GetSymmetricServerEnvelopesAsync()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "/api/keys/symmetric");

            using var response = await _httpclient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var envelopes = await response.Content.ReadFromJsonAsync<List<EncryptedEnvelopeResponseDto>>();

            if (envelopes == null)
                throw new InvalidOperationException("Invalid envelope response from server");

            return envelopes;
        }
        public async Task DeleteSyncedEnvelopeAsync(Guid envelopeId)
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/keys/symmetric/{envelopeId}");

            using var response = await _httpclient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
