using Weardian.Client.Domain.KeyRecords.Symmetric;

namespace Weardian.Client.Core.Interfaces
{
    internal interface ISymmetricKeyRepository
    {
        public Task AddKeyAsync(SymmetricKeyRecord keyRecord);
    }
}
