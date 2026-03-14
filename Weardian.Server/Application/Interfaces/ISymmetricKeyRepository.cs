using Weardian.Server.Domain.Keys.Symmetric;

namespace Weardian.Server.Application.Interfaces
{
    public interface ISymmetricKeyRepository
    {
        public Task AddAsync(SymmetricKey key);
        public Task<IEnumerable<SymmetricKey>> GetAllAsync(string userId);
        public Task<SymmetricKey> GetByIdAsync(string userId, Guid publicId);
        public Task<bool> RemoveByIdAsync(string userId, Guid publicId);
    }
}
