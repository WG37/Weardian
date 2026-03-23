using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Core.Interfaces.Cryptography.Encryption;
using Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping;

namespace Weardian.Client.Infrastructure.Cryptography.KeyWrapping
{
    internal class KeyWrappingService : IKeyWrappingService
    {
        private readonly IAesEncryptor _encryptor;
        private readonly IKekProvider _provider;

        public KeyWrappingService(IAesEncryptor encryptor, IKekProvider provider)
        {
            _encryptor = encryptor;
            _provider  = provider;
        }

        public WrappedKeyResult WrapKey(byte[] dataKey)
        {
            var kek = _provider.GetOrCreateKek();
            var result = _encryptor.Encrypt(dataKey, kek);

            return new WrappedKeyResult(
                Version: 1,
                WrapAlgorithm: "AES-GCM",
                WrappingKeyId: Guid.NewGuid(),
                WrappedKeyTag: result.Tag,
                WrappedKeyNonce: result.Nonce,
                WrappedKeyCiphertext: result.Ciphertext);
        }
        public byte[] UnwrapKey(EncryptedEnvelopeDto envelope)
        {
            throw new NotImplementedException();
        }

    }
}
