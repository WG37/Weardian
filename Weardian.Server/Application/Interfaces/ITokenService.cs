using Weardian.Server.Application.DTOs.Authentication;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(ApplicationUser user);
        public string GenerateRefreshToken();
        public string GenerateHashForRefreshToken(string rawToken);
        public Task<RefreshTokenResponseDto> CreateRefreshTokenAsync(ApplicationUser user, string hashedToken);
        public Task<RefreshTokenResponseDto> ValidateRefreshTokenAsync(string hashedToken);
        public Task<RefreshTokenResponseDto> UpdateRefreshTokenAsync(string hashedToken);
    }
}
