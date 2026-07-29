using Weardian.Client.Core.Interfaces.Cryptography.Encryption;
using Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping;
using Weardian.Client.Domain.KeyRecords.Symmetric;

namespace Weardian.Client.Infrastructure.Cryptography.KeyWrapping
{
    public class KeyWrappingService : IKeyWrappingService
    {
        private readonly IAesEncryptor _encryptor;
        private readonly IKekProvider _provider;

        public KeyWrappingService(IAesEncryptor encryptor, IKekProvider provider)
        {
            _encryptor = encryptor;
            _provider  = provider;
        }

        public async Task<WrappedKeyResult> WrapKey(byte[] dataKey)
        {
            var kek = await _provider.GetOrCreateKekAsync();
            var wrapResult = _encryptor.Encrypt(dataKey, kek);

            return new WrappedKeyResult(
                Version: 1,
                WrapAlgorithm: "AES-GCM",
                WrappingKeyId: _provider.GetKekId(),
                WrappedKeyTag: wrapResult.Tag,
                WrappedKeyNonce: wrapResult.Nonce,
                WrappedKeyCiphertext: wrapResult.Ciphertext);
        }
        public async Task<byte[]> UnwrapKey(KeyRecord keyRecord)
        {
            var currentKekId = _provider.GetKekId();
            
            if (keyRecord.WrappingKeyId != currentKekId)
                throw new InvalidOperationException("Wrapped key does not match the stored Kek.");

            var kek = await _provider.GetOrCreateKekAsync();

            var unWrapResult = _encryptor.Decrypt(
                keyRecord.WrappedKeyCiphertext,
                kek,
                keyRecord.WrappedKeyNonce,
                keyRecord.WrappedKeyTag); 

            return unWrapResult;
        }

    }
}
