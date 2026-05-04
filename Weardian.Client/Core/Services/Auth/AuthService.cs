using System.Net.Http;
using System.Net.Http.Json;
using Weardian.Client.Core.DTOs.AuthDtos.Requests;
using Weardian.Client.Core.DTOs.AuthDtos.Responses;
using Weardian.Client.Core.Interfaces.Auth;
using Weardian.Client.Core.Interfaces.InputValidation;

namespace Weardian.Client.Core.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthTokenStorage _authStorage;
        private readonly IInputValidationService _validationService;

        public AuthService(
            HttpClient httpClient,
            IAuthTokenStorage authStorage,
            IInputValidationService validationService)
        {
            _httpClient = httpClient;
            _authStorage = authStorage;
            _validationService = validationService;
        }

        public async Task RegisterUserAsync(string email, string username, string password)
        {
            var validationResults = _validationService.ValidateRegisterUser(email, username, password);
            
            if (!validationResults.IsValid)
                throw new ArgumentException(string.Join("\n", validationResults.Errors));

            var registerDto = new RegisterRequestDto(email, username, password);

            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", registerDto);

            response.EnsureSuccessStatusCode();
        }

        public async Task LoginAsync(string email, string password)
        {
            var validationResults = _validationService.ValidateLogin(email, password);

            if (!validationResults.IsValid)
                throw new ArgumentException(string.Join("\n", validationResults.Errors));

            var loginDto = new LoginRequestDto(email, password);

            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginDto);

            response.EnsureSuccessStatusCode();

            var authResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            if (authResponse == null || string.IsNullOrEmpty(authResponse.AccessToken))
                throw new InvalidOperationException("Failed to authenticate: Invalid response.");

            await _authStorage.SetAccessTokenAsync(authResponse.AccessToken);
        }

        public async Task LogoutAsync()
        {
             await _authStorage.ClearAccessTokenAsync();
        }
    }
}
