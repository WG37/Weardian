using Weardian.Client.Infrastructure.Cryptography.Encryption;

namespace Weardian.Client.Core.Interfaces.Cryptography.Encryption
{
    internal interface IAesEncryptor
    {
        public EncryptionResult Encrypt(byte[] plaintext, byte[] key);
    }
}
