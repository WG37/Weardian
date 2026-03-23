using Weardian.Client.Core.Interfaces;
using Weardian.Client.Domain.Keys.Symmetric;

namespace Weardian.Client.Infrastructure.Repositories
{
    internal class SymmetricKeyRepository : ISymmetricKeyRepository
    {
        public Task AddKeyAsync(SymmetricKey key)
        {
            throw new NotImplementedException();
        }
    }
}
