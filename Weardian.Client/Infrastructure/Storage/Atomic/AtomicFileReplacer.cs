using System.IO;
using System.Linq.Expressions;

namespace Weardian.Client.Infrastructure.Storage.Atomic
{
    internal static class AtomicFileReplacer
    {
        public static void ReplaceFile(string tempPath, string path)
        {
            try
            {
                File.Replace(tempPath, path, null);
            }
            catch (FileNotFoundException)
            {
                File.Move(tempPath, path);
            }
        }
    }
}
