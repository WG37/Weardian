using Weardian.Server.Application.DTOs.Authentication;
using Weardian.Server.Domain.Authentication;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        public Task AddAsync(RefreshToken token);
        public Task<RefreshToken?> GetByHashAsync(string hashedToken);
        public Task RotateTokensAsync(RefreshToken oldtoken, RefreshToken newToken);
        public Task<IReadOnlyList<RefreshToken>> GetAllUserTokensAsync(ApplicationUser user);
        public Task SaveChangesAsync();
    }
}
