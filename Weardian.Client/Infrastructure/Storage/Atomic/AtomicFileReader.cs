using System.IO;

namespace Weardian.Client.Infrastructure.Storage.Atomic
{
    internal static class AtomicFileReader
    {
        public static async Task<string> ReadFileAsync(string path, string data)
        {

            if (!File.Exists(path))
            {
                return null;
            }

            return await File.ReadAllTextAsync(path);
        }
    }
}
