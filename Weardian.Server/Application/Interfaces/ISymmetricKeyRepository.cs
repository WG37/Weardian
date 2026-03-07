using Weardian.Server.Domain.Keys.SymmetricKeys;

namespace Weardian.Server.Application.Interfaces
{
    public interface ISymmetricKeyRepository
    {
        public Task AddAsync(SymmetricKey entity);
        public Task<IEnumerable<SymmetricKey>> GetAllAsync();
        public Task<SymmetricKey> GetByIdAsync(Guid publicId);
        public Task RemoveByIdAsync(Guid publicId);
    }
}
