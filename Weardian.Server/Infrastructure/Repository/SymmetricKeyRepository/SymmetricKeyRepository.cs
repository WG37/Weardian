using Microsoft.EntityFrameworkCore;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.KeyRecords.Symmetric;
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
        public async Task AddAsync(SymmetricKeyRecord key)
        {
            _db.SymmetricKeyRecords.Add(key);

            await _db.SaveChangesAsync();
        }

        public async Task<SymmetricKeyRecord> GetByIdAsync(string userId, Guid publicId)
        {
            var key = await _db.SymmetricKeyRecords
                .SingleOrDefaultAsync(k => k.UserId == userId && k.PublicId == publicId);

            if (key == null)
                throw new KeyNotFoundException("publicId does not exist on database");

            return key;    
        }

        public async Task<IEnumerable<SymmetricKeyRecord>> GetAllAsync(string userId)
        {
            var keys = await _db.SymmetricKeyRecords.Where(k => k.UserId == userId).ToListAsync();

            return keys;
        }

        public async Task<bool> RemoveByIdAsync(string userId, Guid publicId)
        {
            var key = await _db.SymmetricKeyRecords
                .SingleOrDefaultAsync(k => k.UserId == userId && k.PublicId == publicId);

            if (key == null)
                return false;

            _db.SymmetricKeyRecords.Remove(key);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
