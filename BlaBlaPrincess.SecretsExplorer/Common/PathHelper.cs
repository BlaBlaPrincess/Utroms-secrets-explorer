using System.IO;

namespace BlaBlaPrincess.SecretsExplorer.Common
{
    public static class PathHelper
    {
        public static string GetFileOrDirectoryName(string path)
        {
            return Path.GetFileName(Path.TrimEndingDirectorySeparator(path));
        }

        public static string GetParentName(string path)
        {
            return Path.GetDirectoryName(Path.TrimEndingDirectorySeparator(path));
        }
        
        public static string RenameFile(string path, string name)
        {
            var dir = GetParentName(path);
            return Path.Join(dir, name);
        }
        
        public static string AddIdentifier(string path, int id)
        {
            if (id == 0)
            {
                return path;
            }

            var ex = Path.GetExtension(path);
            var name = Path.GetFileNameWithoutExtension(Path.TrimEndingDirectorySeparator(path));
            return RenameFile(path, $"{name} ({id}){ex}");
        }

        public static string UnifySeparator(string path)
        {
            return path.Replace("\\", "/");
        }
    }
}