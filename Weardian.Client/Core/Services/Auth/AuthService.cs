using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Navigation;
using Weardian.Client.Core.DTOs.Auth.Requests;
using Weardian.Client.Core.DTOs.Auth.Responses;
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

        public async Task<RegistrationResponseDto> RegisterUser(string email, string password)
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

        public async Task<LoginResponseDto> LoginUser(string email, string password)
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
                    Error: authResponse.Error
                    );
            }

            if (string.IsNullOrEmpty(authResponse.Token))
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: "Token is invalid");
            }


            if (string.IsNullOrEmpty(authResponse.RefreshToken))
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: "Refresh token is invalid");
            }

                _authStorage.SetAccessToken(authResponse.Token);
                _authStorage.SetRefreshToken(authResponse.RefreshToken);

            return new LoginResponseDto(
                IsSuccessful: true,
                Error: null);
        }

        public async Task<LoginResponseDto> RefreshUserToken()
        {
            var refreshToken = _authStorage.GetRefreshToken();

            if (string.IsNullOrEmpty(refreshToken))
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: "Refresh token is invalid");
            }

            var response = await _httpClient.PostAsJsonAsync("/api/auth/refresh", refreshToken);

            if (!response.IsSuccessStatusCode)
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: "Login authentication failed");
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthTokenResponseDto>();
            
            if (authResponse == null)
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: "Authentication response is null");
            }

            if (!authResponse.IsSuccessful)
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: authResponse.Error);
            }

            if (string.IsNullOrEmpty(authResponse.Token))
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: "Invalid token");
            }

            if (string.IsNullOrEmpty(authResponse.RefreshToken))
            {
                return new LoginResponseDto(
                    IsSuccessful: false,
                    Error: "Invalid refresh token");
            }

            _authStorage.SetAccessToken(authResponse.Token);
            _authStorage.SetRefreshToken(authResponse.RefreshToken);

            return new LoginResponseDto(
                IsSuccessful: true,
                Error: null);
        }

        public LogoutResponseDto LogoutUser()
        {
            _authStorage.ClearAccessToken();
            _authStorage.ClearRefreshToken();

            return new LogoutResponseDto(
                IsSuccessful: true);
        }
    }
}
