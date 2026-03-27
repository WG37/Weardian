using System.IO;
using Weardian.Client.Infrastructure.Native.PathBuilder;
using Weardian.Client.Infrastructure.Repositories.DTOs;
using Weardian.Client.Infrastructure.Serialization;
using Weardian.Client.Infrastructure.Storage.Atomic;

namespace Weardian.Client.Infrastructure.Native.Bootstrapper
{
    internal static class VaultBootstrap
    {
        private static readonly int Version = 1;

        public static async Task EnsureInitializedAsync()
        {
            Directory.CreateDirectory(AppDataPaths.RootDir);
            Directory.CreateDirectory(AppDataPaths.VaultDir);
            Directory.CreateDirectory(AppDataPaths.KeysDir);
            Directory.CreateDirectory(AppDataPaths.BlobsDir);

            if (!File.Exists(AppDataPaths.SettingsPath))
            {
                var settings = new VaultSettingsDto(
                    VaultId: Guid.NewGuid(),
                    SchemaVersion: Version,
                    CreatedOn: DateTime.UtcNow);

                var data = JsonSerializerHelper.Serialize(settings);
                await AtomicFileWriter.WriteToFileAsync(AppDataPaths.SettingsPath, data);
            };
            
            if (!File.Exists(AppDataPaths.VaultIndexPath))
            {
                var index = new VaultIndexDto
                {
                    SchemaVersion = Version,
                    CreatedOn = DateTime.UtcNow,
                    Keys = Array.Empty<VaultKeyIndexDto>()
                };

                var data = JsonSerializerHelper.Serialize(index);
                await AtomicFileWriter.WriteToFileAsync(AppDataPaths.VaultIndexPath, data);
            }
        }
    }
}
