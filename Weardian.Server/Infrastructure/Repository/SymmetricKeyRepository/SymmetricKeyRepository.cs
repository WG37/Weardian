using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Keys.SymmetricKeys;
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
        public async Task AddAsync(SymmetricKey entity)
        {
            _db.SymmetricKeys.Add(entity);

            await _db.SaveChangesAsync();
        }

        public async Task<SymmetricKey> GetByIdAsync(Guid publicId)
        {
            var entity = await _db.SymmetricKeys.SingleOrDefaultAsync(k => k.Id == publicId);
            if (entity == null)
                throw new KeyNotFoundException("publicId does not exist on database");

            return entity;    
        }

        public async Task<IEnumerable<SymmetricKey>> GetAllAsync()
        {
            var entities = await _db.SymmetricKeys.ToListAsync();
            if (entities == null)
                throw new KeyNotFoundException("No keys exist on the database");

            return entities;
        }

        public async Task RemoveByIdAsync(Guid publicId)
        {
            var entity = await _db.SymmetricKeys.SingleOrDefaultAsync(k => k.Id == publicId);
            if (entity == null)
                throw new KeyNotFoundException($"ID: {publicId} does not exist on database");

            _db.SymmetricKeys.Remove(entity);
            await _db.SaveChangesAsync();
        }

    }
}
