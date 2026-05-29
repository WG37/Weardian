using Weardian.Server.Domain.EncryptedEnvelopes.Symmetric;
using Weardian.Server.Domain.KeyRecords.Symmetric;

namespace Weardian.Server.Application.Interfaces
{
    public interface ISymmetricEnvelopeRepository
    {
        public Task AddAsync(SymmetricEncryptedEnvelope envelope);
        public Task<IReadOnlyList<SymmetricEncryptedEnvelope>> GetAllAsync(string userId);
        public Task<SymmetricEncryptedEnvelope> GetByIdAsync(string userId, Guid envelopeId);
        public Task<bool> RemoveByIdAsync(string userId, Guid envelopeId);
    }
}
