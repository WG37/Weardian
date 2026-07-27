using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Weardian.Client.Core.DTOs.AuthDtos.Requests;
using Weardian.Client.Core.DTOs.AuthDtos.Responses;
using Weardian.Client.Core.Interfaces.Auth;
using Weardian.Client.Core.Interfaces.InputValidation;
using Weardian.Client.Core.Serialization;

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

        public async Task<RegistrationResponseDto> RegisterUserAsync(string email, string password)
        {
            var validationResults = _validationService.ValidateRegisterUser(email, password);
            
            if (!validationResults.IsValid)
            {
                return new RegistrationResponseDto(
                    IsSuccessful: false,
                    Error: string.Join("\n", validationResults.Errors));
            }

            var registerDto = new RegisterRequestDto(email, password);

            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", registerDto);

            var result = await response.Content.ReadFromJsonAsync<RegistrationResponseDto>(
                JsonSerializeCaseHelper.CaseInsensitiveOptions);
           
            if (result == null)
            {
                return new RegistrationResponseDto(
                    IsSuccessful: false,
                    Error: "Failed to deserialize registration response");
            }

            return result;
        }

        public async Task<LoginResponseDto> LoginAsync(string email, string password)
        {
            var validationResults = _validationService.ValidateLogin(email, password);

            if (!validationResults.IsValid)
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: string.Join("\n", validationResults.Errors));
            }

            var loginDto = new LoginRequestDto(email, password);

            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginDto);

            if (!response.IsSuccessStatusCode)
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: "Invalid Credentials."
                    );
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthTokenResponseDto>();

            if (authResponse == null)
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: "Invalid response from server");
            }

            if (!authResponse.IsSuccessful)
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    authResponse.Error
                    );
            }

            if (string.IsNullOrEmpty(authResponse.Token))
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: "Token is invalid");
            }

            await _authStorage.SetAccessTokenAsync(authResponse.Token);

            return new LoginResponseDto(
                IsSuccessful: true,
                Error: null);
        }

        public async Task LogoutAsync()
        {
             await _authStorage.ClearAccessTokenAsync();
        }
    }
}
