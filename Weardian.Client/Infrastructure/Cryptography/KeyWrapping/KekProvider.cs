using System.IO;
using System.Security.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping;
using Weardian.Client.Infrastructure.Native.PathBuilder;
using Weardian.Client.Infrastructure.Storage.Atomic;

namespace Weardian.Client.Infrastructure.Cryptography.KeyWrapping
{
    internal sealed class KekProvider : IKekProvider
    {
        private const int KekBytes = 32;

        public async Task<byte[]> CreateKekAsync()
        {
            Directory.CreateDirectory(AppDataPaths.DataProtectionDir);

            if (File.Exists(AppDataPaths.KekPath))
                throw new InvalidOperationException("Kek already exists");

            var kekId = Guid.NewGuid();
            var kek = RandomNumberGenerator.GetBytes(KekBytes);

            var protectedBytes = ProtectedData.Protect(
                kek,
                optionalEntropy: null,
                scope: DataProtectionScope.CurrentUser);

            await AtomicFileWriter.WriteToFileAsync(AppDataPaths.KekPath, protectedBytes);
            
            var metadata = new KekMetadata(kekId, Version: 1, DateTime.UtcNow);
            var json = Serialization.JsonSerializerHelper.Serialize(metadata);

            await AtomicFileWriter.WriteToFileAsync(AppDataPaths.KekMetadataPath, json);

            return kek;
        }

        public byte[] GetKek()
        {
            Directory.CreateDirectory(AppDataPaths.DataProtectionDir);

            if (!File.Exists(AppDataPaths.KekPath))
                throw new InvalidOperationException("Kek does not exist");

            var protectedKek = File.ReadAllBytes(AppDataPaths.KekPath);

            return ProtectedData.Unprotect(
                protectedKek,
                optionalEntropy: null,
                DataProtectionScope.CurrentUser);
        }

        public async Task<byte[]> GetOrCreateKekAsync()
        {
            if (File.Exists(AppDataPaths.KekPath))
            {
                return GetKek();
            }

            return await CreateKekAsync();
        }

        public Guid GetKekId()
        {
            if (!File.Exists(AppDataPaths.KekMetadataPath))
                throw new InvalidOperationException("Kek metadata does not exist.");

            var json = File.ReadAllText(AppDataPaths.KekMetadataPath);
            var metadata = Serialization.JsonSerializerHelper.Deserialize<KekMetadata>(json)
                ?? throw new InvalidOperationException("Failed to deserialize Kek metadata.");

            return metadata.KekId;
        }
    }
}
