using Weardian.Client.Infrastructure.Cryptography.Encryption;

namespace Weardian.Client.Core.Interfaces.Cryptography.Encryption
{
    public interface IAesEncryptor
    {
        public PayloadResult Encrypt(byte[] plaintext, byte[] key);
        public byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] nonce, byte[] tag);
    }
}
