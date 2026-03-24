using System.Security.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography.Encryption;

namespace Weardian.Client.Infrastructure.Cryptography.Encryption
{
    internal class AesEncryptor : IAesEncryptor
    {
        public EncryptionResult Encrypt(byte[] plaintext, byte[] key)
        {
            var nonce = new byte[12];
            RandomNumberGenerator.Fill(nonce);

            var ciphertext = new byte[plaintext.Length];
            var tag = new byte[16];

            using (var aes = new AesGcm(key, tag.Length))
            {
                aes.Encrypt(nonce, plaintext, ciphertext, tag);
            }

            return new EncryptionResult(
                Nonce: nonce,
                Tag: tag,
                Ciphertext: ciphertext);
        }

        public byte[] Decrypt(byte[] ciphertext, byte[] key)
        {
            var nonce = new byte[12];
            var encryptedData = new byte[ciphertext.Length - 28];
            var tag = new byte[16];

            var plaintext = new byte[encryptedData.Length];

            Array.Copy(ciphertext, 0, nonce, 0, 12);
            Array.Copy(ciphertext, 12, encryptedData, 0, encryptedData.Length);
            Array.Copy(ciphertext, ciphertext.Length - 16, tag, 0, 16);

            using (var aes = new AesGcm(key, tag.Length))
            {
                aes.Decrypt(nonce, ciphertext, tag, plaintext);
            }

            return plaintext;
        }
    }
}
