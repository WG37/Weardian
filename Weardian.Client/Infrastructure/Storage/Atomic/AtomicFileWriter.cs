using System.IO;
using System.Text;

namespace Weardian.Client.Infrastructure.Storage.Atomic
{
    internal static class AtomicFileWriter
    {
        public static async Task WriteToFileAsync(string path, string data)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)
                ?? throw new InvalidOperationException("Failed to resolve the specified path."));

            var tmp = path + ".tmp";

            await File.WriteAllTextAsync(tmp, data, new UTF8Encoding(false));

            AtomicFileReplacer.ReplaceFile(tmp, path);
        }
    }
}
