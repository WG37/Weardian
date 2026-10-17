using Weardian.Server.Application.DTOs.Authentication;
using Weardian.Server.Domain.Authentication;

namespace Weardian.Server.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        public Task AddAsync(RefreshToken token);
        public Task<RefreshToken?> GetRefreshTokenByHashAsync(string hashedToken);
    }
}
