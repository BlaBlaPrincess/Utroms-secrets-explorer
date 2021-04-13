using System.IO;

namespace BlaBlaPrincess.SecretsExplorer.Common
{
    public static class PathHelper
    {
        public static string GetFileOrDirectoryName(string path)
        {
            return Path.GetFileName(Path.TrimEndingDirectorySeparator(path));
        }
    }
}