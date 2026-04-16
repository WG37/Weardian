using System.Text;
using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Core.Interfaces.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography.Encryption;
using Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping;

namespace Weardian.Client.Infrastructure.Cryptography
{
    internal class SymmetricCryptoService : ISymmetricCryptoService
    {
        private readonly IAesEncryptor _encryptor;
        private readonly IKeyGeneration _keyGen;
        private readonly IKeyWrappingService _keyWrap;

        public SymmetricCryptoService(IAesEncryptor encryptor, 
            IKeyGeneration keyGen,
            IKeyWrappingService keyWrap)
        {
            _encryptor = encryptor;
            _keyGen = keyGen;
            _keyWrap = keyWrap;
        }

        public async Task<EncryptedEnvelopeDto> CreateEncryptedEnvelopeAsync(string plaintext)
        {
            var ptBytes = Encoding.UTF8.GetBytes(plaintext);

            var dataKey = _keyGen.GenerateSymmetricKey()
                ?? throw new InvalidOperationException("Failed to generate symmetric key");
            
            var encryptedResults = _encryptor.Encrypt(ptBytes, dataKey)
                ?? throw new InvalidOperationException("Encryption operation failed.");

            var wrappedResults = await _keyWrap.WrapKey(dataKey)
                ?? throw new InvalidOperationException("Key wrapping operation failed");

            return new EncryptedEnvelopeDto(
                EnvelopeId: Guid.NewGuid(),
                new WrappedKeyDto(
                wrappedResults.Version,
                wrappedResults.WrapAlgorithm,
                wrappedResults.WrappingKeyId,
                wrappedResults.WrappedKeyCiphertext,
                wrappedResults.WrappedKeyTag,
                wrappedResults.WrappedKeyNonce),
                new PayloadRecordDto(
                encryptedResults.Version,
                encryptedResults.Algorithm,
                encryptedResults.Ciphertext,
                encryptedResults.Tag,
                encryptedResults.Nonce));
        }
    }
}
