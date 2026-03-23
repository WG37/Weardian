using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Infrastructure.Cryptography.KeyWrapping;

namespace Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping
{
    internal interface IKeyWrappingService
    {
        public WrappedKeyResult WrapKey(byte[] dataKey);
        public byte[] UnwrapKey(EncryptedEnvelopeDto envelope);
    }
}
