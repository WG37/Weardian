using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Weardian.Client.Core.DTOs.KeySyncingDtos;
using Weardian.Client.Core.Interfaces.Auth;
using Weardian.Client.Core.Interfaces.Sync;
using Weardian.Client.Domain.KeyRecords.Symmetric;

namespace Weardian.Client.Core.Services.Sync
{
    public class KeyRecordSyncService : IKeyRecordSyncService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthTokenStorage _authToken;

        public KeyRecordSyncService(
            HttpClient httpClient,
            IAuthTokenStorage authToken)
        {
            _httpClient = httpClient;
            _authToken = authToken;
        }

        public async Task<KeySyncResponseDto> SyncKeyRecordAsync(KeyRecord keyRecord)
        {

            var token = await _authToken.GetAccessTokenAsync();

            var dto = new KeySyncRequestDto(
                EnvelopeId: keyRecord.EnvelopeId,
                Name: keyRecord.Name,
                EnvelopeVersion: keyRecord.EnvelopeVersion,
                WrapAlgorithm: keyRecord.WrapAlgorithm,
                WrappingKeyId: keyRecord.WrappingKeyId,
                WrappedKeyNonce: keyRecord.WrappedKeyNonce,
                WrappedKeyCiphertext: keyRecord.WrappedKeyCiphertext,
                WrappedKeyTag: keyRecord.WrappedKeyTag,
                CreatedOn: keyRecord.CreatedOn);

            using var request = new HttpRequestMessage(HttpMethod.Post, "/api/keys/symmetric");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = JsonContent.Create(dto);

            using var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var syncResponse = await response.Content.ReadFromJsonAsync<KeySyncResponseDto>();

            if (syncResponse == null)
                throw new InvalidOperationException("Invalid key sync response");

            return syncResponse;
        }
    }
}
