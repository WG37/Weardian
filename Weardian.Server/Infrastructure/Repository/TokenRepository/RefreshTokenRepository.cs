using Microsoft.EntityFrameworkCore;
using Weardian.Server.Application.DTOs.Authentication;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Authentication;
using Weardian.Server.Infrastructure.Data;

namespace Weardian.Server.Infrastructure.Repository.TokenRepository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _db;

        public RefreshTokenRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(RefreshToken token)
        {
            _db.RefreshTokens.Add(token);

            await _db.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByHashAsync(string hashedToken)
        {
            var tokenMatch = await _db.RefreshTokens
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(rt => rt.HashedToken == hashedToken);

            return tokenMatch;
        }

        public async Task RotateTokensAsync(RefreshToken oldToken, RefreshToken newToken)
        {
            await _db.RefreshTokens.AddAsync(newToken);
            _db.RefreshTokens.Update(oldToken);

            await _db.SaveChangesAsync();
        }
    }
}
