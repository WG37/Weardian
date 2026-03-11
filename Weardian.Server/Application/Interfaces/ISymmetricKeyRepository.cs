using Weardian.Server.Domain.Keys.Symmetric;

namespace Weardian.Server.Application.Interfaces
{
    public interface ISymmetricKeyRepository
    {
        public Task AddAsync(SymmetricKey key);
        public Task<IEnumerable<SymmetricKey>> GetAllAsync();
        public Task<SymmetricKey> GetByIdAsync(Guid publicId);
        public Task<bool> RemoveByIdAsync(Guid publicId);
    }
}
