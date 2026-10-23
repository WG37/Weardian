using Weardian.Client.Core.DTOs.Auth.Responses;

namespace Weardian.Client.Core.Interfaces.Auth
{
    public interface IAuthService
    {
        public Task<RegistrationResponseDto> RegisterUser(string email, string password);
        public Task<LoginResponseDto> LoginUser(string email, string password);
        public Task<LoginResponseDto> RefreshUserToken();
        public LogoutResponseDto LogoutUser();
    }
}
