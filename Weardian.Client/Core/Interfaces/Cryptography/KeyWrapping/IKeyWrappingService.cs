using Weardian.Client.Domain.KeyRecords.Symmetric;
using Weardian.Client.Infrastructure.Cryptography.KeyWrapping;

namespace Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping
{
    public interface IKeyWrappingService
    {
        public Task<WrappedKeyResult> WrapKey(byte[] dataKey);
        public Task<byte[]> UnwrapKey(KeyRecord keyRecord);
    }
}
