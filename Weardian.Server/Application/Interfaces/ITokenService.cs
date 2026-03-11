using Weardian.Server.Domain.Users;

namespace Weardian.Server.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(ApplicationUser user, IList<string> roles);
    }
}
