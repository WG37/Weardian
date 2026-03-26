using Weardian.Server.Domain.KeyRecords.Symmetric;

namespace Weardian.Server.Application.Interfaces
{
    public interface ISymmetricKeyRepository
    {
        public Task AddAsync(SymmetricKeyRecord keyRecord);
        public Task<IEnumerable<SymmetricKeyRecord>> GetAllAsync(string userId);
        public Task<SymmetricKeyRecord> GetByIdAsync(string userId, Guid envelopeId);
        public Task<bool> RemoveByIdAsync(string userId, Guid envelopeId);
    }
}
