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
            var wrapResult = _encryptor.Encrypt(dataKey, kek)
                ?? throw new InvalidOperationException();

            return new WrappedKeyResult(
                Version: 1,
                WrapAlgorithm: "AES-GCM",
                WrappingKeyId: Guid.NewGuid(),
                WrappedKeyTag: wrapResult.Tag,
                WrappedKeyNonce: wrapResult.Nonce,
                WrappedKeyCiphertext: wrapResult.Ciphertext);
        }
        public byte[] UnwrapKey(EncryptedEnvelopeDto envelope)
        {
            var kek = _provider.GetOrCreateKek();
            var unWrapResult = _encryptor.Decrypt(envelope.WrappedKey.WrappedKeyCiphertext, kek);

            return unWrapResult;
        }

    }
}
