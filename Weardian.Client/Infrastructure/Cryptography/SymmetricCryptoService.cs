using System.Security.Cryptography;
using System.Text;
using Weardian.Client.Core.DTOs.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography.Encryption;
using Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping;
using Weardian.Client.Domain.KeyRecords.Symmetric;
using Weardian.Client.Domain.PayloadRecords.Symmetric;

namespace Weardian.Client.Infrastructure.Cryptography
{
    public class SymmetricCryptoService : ISymmetricCryptoService
    {
        private readonly IAesEncryptor _encryptor;
        private readonly IKeyGeneration _keyGen;
        private readonly IKeyWrappingService _keyWrap;

        public SymmetricCryptoService(
            IAesEncryptor encryptor, 
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

        public async Task<string> DecryptEncryptedEnvelopeAsync(KeyRecord keyRecord, PayloadRecord payloadRecord)
        {
            var dataKey = await _keyWrap.UnwrapKey(keyRecord)
                ?? throw new InvalidOperationException("Failed to unwrap the data key.");

            var ptBytes = _encryptor.Decrypt(payloadRecord.Ciphertext, dataKey, payloadRecord.Nonce, payloadRecord.Tag)
                ?? throw new InvalidOperationException("Failed to decrypt the payload data");

            try
            {
                return Encoding.UTF8.GetString(ptBytes);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(ptBytes);
                CryptographicOperations.ZeroMemory(dataKey);
            }
        }
    }
}
