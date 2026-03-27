using System.IO;

namespace Weardian.Client.Infrastructure.Native.PathBuilder
{
    internal static class AppDataPaths
    {
        private const string MainFolder = "Weardian";
        private const string VaultFolder = "Vault";

        private static readonly string _appDataRoaming =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string RootDir =>
            Path.Combine(_appDataRoaming, MainFolder);

        public static string VaultDir =>
            Path.Combine(RootDir, VaultFolder);

        public static string KeysDir =>
            Path.Combine(VaultDir, "keys");

        public static string BlobsDir =>
            Path.Combine(VaultDir, "blobs");

        public static string DataProtectionDir =>
            Path.Combine(RootDir, "Data", "Dpapi");

        public static string SettingsPath =>
            Path.Combine(VaultDir, "settings.json");

        public static string VaultIndexPath =>
            Path.Combine(VaultDir, "vault.json");

        public static string KekPath =>
            Path.Combine(DataProtectionDir, "kek.bin");

        public static string BlobPath(Guid envelopeId) =>
            Path.Combine(BlobsDir, $"{envelopeId}.blob");

        public static string KeyRecordPath(Guid envelopeId) =>
            Path.Combine(KeysDir, $"{envelopeId}.enc");
    }
}
