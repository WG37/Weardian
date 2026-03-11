using Microsoft.EntityFrameworkCore;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Keys.Symmetric;
using Weardian.Server.Infrastructure.Data;

namespace Weardian.Server.Infrastructure.Repository.SymmetricKeyRepository
{
    public class SymmetricKeyRepository : ISymmetricKeyRepository
    {
        private readonly AppDbContext _db;

        public SymmetricKeyRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(SymmetricKey key)
        {
            _db.SymmetricKeys.Add(key);

            await _db.SaveChangesAsync();
        }

        public async Task<SymmetricKey> GetByIdAsync(Guid publicId)
        {
            var key = await _db.SymmetricKeys.SingleOrDefaultAsync(k => k.PublicId == publicId);
            if (key == null)
                throw new KeyNotFoundException("publicId does not exist on database");

            return key;    
        }

        public async Task<IEnumerable<SymmetricKey>> GetAllAsync()
        {
            var keys = await _db.SymmetricKeys.ToListAsync();
            if (keys == null)
                throw new KeyNotFoundException("No keys exist on the database");

            return keys;
        }

        public async Task<bool> RemoveByIdAsync(Guid publicId)
        {
            var key = await _db.SymmetricKeys.SingleOrDefaultAsync(k => k.PublicId == publicId);
            if (key == null)
                return false;

            _db.SymmetricKeys.Remove(key);
            await _db.SaveChangesAsync();

            return true;
        }

    }
}
