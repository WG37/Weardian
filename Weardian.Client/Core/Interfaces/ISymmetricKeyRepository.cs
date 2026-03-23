using Weardian.Client.Domain.Keys.Symmetric;

namespace Weardian.Client.Core.Interfaces
{
    internal interface ISymmetricKeyRepository
    {
        public Task AddKeyAsync(SymmetricKey key);
    }
}
