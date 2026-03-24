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

        public EncryptedEnvelopeDto CreateEncryptedEnvelope(string plaintext)
        {
            var ptBytes = Encoding.UTF8.GetBytes(plaintext);
            var dataKey = _keyGen.GenerateSymmetricKey();
            
            var encryptedResults = _encryptor.Encrypt(ptBytes, dataKey);
            var wrappedResults = _keyWrap.WrapKey(dataKey);

            return new EncryptedEnvelopeDto(
                wrappedResults.Version,
                wrappedResults.WrapAlgorithm,
                wrappedResults.WrappingKeyId,
                wrappedResults.WrappedKeyCiphertext,
                wrappedResults.WrappedKeyTag,
                wrappedResults.WrappedKeyNonce,
                encryptedResults.Ciphertext,
                encryptedResults.Tag,
                encryptedResults.Nonce);
        }
    }
}
