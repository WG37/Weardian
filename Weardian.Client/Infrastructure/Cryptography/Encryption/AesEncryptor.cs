using System.Security.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography.Encryption;

namespace Weardian.Client.Infrastructure.Cryptography.Encryption
{
    internal class AesEncryptor : IAesEncryptor
    {
        public PayloadResult Encrypt(byte[] plaintext, byte[] key)
        {
            var nonce = new byte[12];
            RandomNumberGenerator.Fill(nonce);

            var ciphertext = new byte[plaintext.Length];
            var tag = new byte[16];

            using (var aes = new AesGcm(key, tag.Length))
            {
                aes.Encrypt(nonce, plaintext, ciphertext, tag);
            }

            return new PayloadResult(
                Version: 1,
                Algorithm: "AES-GCM",
                Nonce: nonce,
                Tag: tag,
                Ciphertext: ciphertext);
        }

        public byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] nonce, byte[] tag)
        {
            var plaintext = new byte[ciphertext.Length];

            using (var aes = new AesGcm(key, tag.Length))
            {
                aes.Decrypt(nonce, ciphertext, tag, plaintext);
            }

            return plaintext;
        }
    }
}
