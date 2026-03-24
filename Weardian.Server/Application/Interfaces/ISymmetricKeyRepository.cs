using Weardian.Server.Domain.KeyRecords.Symmetric;

namespace Weardian.Server.Application.Interfaces
{
    public interface ISymmetricKeyRepository
    {
        public Task AddAsync(SymmetricKeyRecord key);
        public Task<IEnumerable<SymmetricKeyRecord>> GetAllAsync(string userId);
        public Task<SymmetricKeyRecord> GetByIdAsync(string userId, Guid publicId);
        public Task<bool> RemoveByIdAsync(string userId, Guid publicId);
    }
}
