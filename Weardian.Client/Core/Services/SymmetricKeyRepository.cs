using Weardian.Client.Core.Interfaces;
using Weardian.Client.Domain.KeyRecords.Symmetric;

namespace Weardian.Client.Core.Services
{
    internal class SymmetricKeyRepository : ISymmetricKeyRepository
    {
        public Task AddKeyAsync(SymmetricKeyRecord key)
        {
            throw new NotImplementedException();
        }
    }
}
