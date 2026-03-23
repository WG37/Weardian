using System.Text;
using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Core.Interfaces.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography.Encryption;

namespace Weardian.Client.Infrastructure.Cryptography
{
    internal class SymmetricCryptoService : ISymmetricCryptoService
    {
        private readonly IAesEncryptor _encryptor;
        private readonly IKeyGeneration _keyGen;

        public SymmetricCryptoService(IAesEncryptor encryptor, IKeyGeneration keyGen)
        {
            _encryptor = encryptor;
            _keyGen = keyGen;
        }

        public EncryptedEnvelopeDto CreateEncryptedEnvelope(string plaintext)
        {
            var ptBytes = Encoding.UTF8.GetBytes(plaintext);
            var key = _keyGen.GenerateSymmetricKey();
            var encryptedResults = _encryptor.Encrypt(ptBytes, key);

            
        }
    }
}
