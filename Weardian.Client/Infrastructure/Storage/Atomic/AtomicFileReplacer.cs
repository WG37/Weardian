using System.IO;

namespace Weardian.Client.Infrastructure.Storage.Atomic
{
    internal static class AtomicFileReplacer
    {
        public static void ReplaceFile(string tempPath, string path)
        {
            if (File.Exists(path))
            {
                File.Replace(tempPath, path, null);
            }
            else
            {

                File.Move(tempPath, path);
            }
        }
    }
}
