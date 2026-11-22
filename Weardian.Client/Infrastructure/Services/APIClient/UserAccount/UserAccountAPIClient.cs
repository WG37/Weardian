using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Serialization;
using System.Text.Json;
using Weardian.Client.Core.DTOs.Auth.Requests;
using Weardian.Client.Core.DTOs.Auth.Responses;
using Weardian.Client.Core.Interfaces.APIClient.UserAccount;

namespace Weardian.Client.Infrastructure.Services.APIClient.UserAccount
{
    public class UserAccountAPIClient : IUserAccountAPIClient
    {
        private readonly HttpClient _client;

        public UserAccountAPIClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<UpdatePasswordResponseDto> SendUpdatePasswordRequestAsync(UpdatePasswordRequestDto updatePasswordRequestDto)
        {
            using var request = new HttpRequestMessage(HttpMethod.Patch, "/api/accounts/user/password");
            request.Content = JsonContent.Create(updatePasswordRequestDto);

            var response = await _client.SendAsync(request);

            var status = response.StatusCode;
            var body = await response.Content.ReadAsStringAsync();
            
            response.EnsureSuccessStatusCode();

            var updatePasswordResponse = await response.Content.ReadFromJsonAsync<UpdatePasswordResponseDto>();

            if (updatePasswordResponse == null)
                throw new JsonException("Failed to deserialize the response");

            return updatePasswordResponse;
        }
    }
}
