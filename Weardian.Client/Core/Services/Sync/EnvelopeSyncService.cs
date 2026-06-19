using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Weardian.Client.Core.DTOs.EnvelopeSyncingDtos.RequestDtos;
using Weardian.Client.Core.DTOs.EnvelopeSyncingDtos.ResponseDtos;
using Weardian.Client.Core.Interfaces.Auth;
using Weardian.Client.Core.Interfaces.Sync;

namespace Weardian.Client.Core.Services.Sync
{
    public class EnvelopeSyncService : IEnvelopeSyncService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthTokenStorage _authToken;

        public EnvelopeSyncService(
            HttpClient httpClient,
            IAuthTokenStorage authToken)
        {
            _httpClient = httpClient;
            _authToken = authToken;
        }

        public async Task<EncryptedEnvelopeSyncResponseDto> SyncEncryptedEnvelopeAsync(EncryptedEnvelopeSyncRequestDto envelopeRequest)
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
    }
}
