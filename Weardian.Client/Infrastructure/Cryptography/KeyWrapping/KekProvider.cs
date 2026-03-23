using System.IO;
using System.Security.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping;
using Weardian.Client.Infrastructure.Native.PathBuilder;

namespace Weardian.Client.Infrastructure.Cryptography.KeyWrapping
{
    internal sealed class KekProvider : IKekProvider
    {
        private const int KekBytes = 32;

        public byte[] CreateKek()
        {
            Directory.CreateDirectory(AppDataPaths.DataProtectionDir);

            if (File.Exists(AppDataPaths.KekPath))
                throw new InvalidOperationException("Kek already exists");

            var kek = RandomNumberGenerator.GetBytes(KekBytes);

            var protectedBytes = ProtectedData.Protect(
                kek,
                optionalEntropy: null,
                scope: DataProtectionScope.CurrentUser);

            File.WriteAllBytes(AppDataPaths.KekPath, protectedBytes);

            return kek;
        }

        public byte[] GetKek()
        {
            if (!File.Exists(AppDataPaths.KekPath))
                throw new InvalidOperationException("Kek does not exist");

            var protectedKek = File.ReadAllBytes(AppDataPaths.KekPath);

            return ProtectedData.Unprotect(
                protectedKek,
                optionalEntropy: null,
                DataProtectionScope.CurrentUser);
        }

        public byte[] GetOrCreateKek()
        {
            if (File.Exists(AppDataPaths.KekPath))
            {
                return GetKek();
            }

            return CreateKek();
        }
    }
}
