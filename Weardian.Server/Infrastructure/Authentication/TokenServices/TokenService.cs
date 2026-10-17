using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Weardian.Server.Application.DTOs.Authentication;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Authentication;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.Infrastructure.Authentication.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly IRefreshTokenRepository _refreshToken;

        public TokenService(IConfiguration config, IRefreshTokenRepository refreshToken)
        {
            _config = config;
            _refreshToken = refreshToken;
        }
        public string GenerateAccessToken(ApplicationUser user)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwt["AccessTokenMinutes"]!));
            
            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var rawTokenBytes = RandomNumberGenerator.GetBytes(32);

            return Convert.ToBase64String(rawTokenBytes);
        }

        public string GenerateHashForRefreshToken(string rawToken)
        {
            var rawTokenBytes = Encoding.UTF8.GetBytes(rawToken);
            var hashTokenBytes = SHA256.HashData(rawTokenBytes);
            var hashedToken = Convert.ToHexString(hashTokenBytes).ToLowerInvariant();

            return hashedToken;
        }

        public async Task CreateRefreshTokenAsync(ApplicationUser user, string hashedToken)
        {
            var hashedRefreshedToken = new RefreshToken
            {
                UserId = user.Id,
                HashedToken = hashedToken,
                Expiry = DateTime.UtcNow.AddDays(28)
            };

            await _refreshToken.AddAsync(hashedRefreshedToken);
        }

        public async Task<RefreshTokenResponseDto> ValidateRefreshTokenAsync(string hashedToken)
        {
            var token = await _refreshToken.GetRefreshTokenByHashAsync(hashedToken);

            if (token == null)
                return new RefreshTokenResponseDto(
                    UserId: null,
                    IsSuccessful: false,
                    Error: "Refresh token does not exist");

            if (token.Expiry <= DateTime.UtcNow)
                return new RefreshTokenResponseDto(
                    UserId: null,
                    IsSuccessful: false,
                    Error: "Refresh token has expired");

            if (token.RevokedAt != null)
                return new RefreshTokenResponseDto(
                    UserId: null,
                    IsSuccessful: false,
                    Error: "Refresh token revoked");

            return new RefreshTokenResponseDto(
                UserId: token.UserId,
                IsSuccessful: true,
                Error: null);
        }
    }
}
