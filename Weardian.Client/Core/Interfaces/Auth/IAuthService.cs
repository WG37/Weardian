using Weardian.Client.Core.DTOs.AuthDtos.Requests;
using Weardian.Client.Core.DTOs.AuthDtos.Responses;

namespace Weardian.Client.Core.Interfaces.Auth
{
    public interface IAuthService
    {
        public Task RegisterUserAsync(string email, string password);
        public Task LoginAsync(string email, string password);
        public Task LogoutAsync();
    }
}
