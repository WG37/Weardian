using Microsoft.EntityFrameworkCore;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.EncryptedEnvelopes.Symmetric;
using Weardian.Server.Infrastructure.Data;

namespace Weardian.Server.Infrastructure.Repository.SymmetricKeyRepository
{
    public class SymmetricEnvelopeRepository : ISymmetricEnvelopeRepository
    {
        private readonly AppDbContext _db;

        public SymmetricEnvelopeRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(SymmetricEncryptedEnvelope envelope)
        {
            _db.SymmetricEncryptedEnvelopes.Add(envelope);

            await _db.SaveChangesAsync();
        }

        public async Task<SymmetricEncryptedEnvelope?> GetByIdAsync(string userId, Guid envelopeId)
        {
            var envelope = await _db.SymmetricEncryptedEnvelopes
                .SingleOrDefaultAsync(k => k.UserId == userId && k.EnvelopeId == envelopeId);

            return envelope;    
        }

        public async Task<IReadOnlyList<SymmetricEncryptedEnvelope>> GetAllAsync(string userId)
        {
            var envelopes = await _db.SymmetricEncryptedEnvelopes.Where(k => k.UserId == userId).ToListAsync();

            return envelopes;
        }

        public async Task<bool> RemoveByIdAsync(string userId, Guid envelopeId)
        {
            var envelope = await _db.SymmetricEncryptedEnvelopes
                .SingleOrDefaultAsync(k => k.UserId == userId && k.EnvelopeId == envelopeId);

            if (envelope == null)
                return false;

            _db.SymmetricEncryptedEnvelopes.Remove(envelope);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
