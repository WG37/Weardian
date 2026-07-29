using Weardian.Client.Core.DTOs.Auth.Responses;

namespace Weardian.Client.Core.Interfaces.Auth
{
    public interface IAuthService
    {
        public Task<RegistrationResponseDto> RegisterUserAsync(string email, string password);
        public Task<LoginResponseDto> LoginAsync(string email, string password);
        public Task<LogoutResponseDto> LogoutAsync();
    }
}
