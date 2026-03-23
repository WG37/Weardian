using System.Security.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography;

namespace Weardian.Client.Infrastructure.Cryptography
{
    internal class KeyGeneration : IKeyGeneration
    {
        public byte[] GenerateSymmetricKey(int length = 32)
        {
            var keyBytes = new byte[length];
            using (var csprng = RandomNumberGenerator.Create())
            {
                csprng.GetBytes(keyBytes);
            }

            return keyBytes;
        }
    }
}
