using System.IO;
using BlaBlaPrincess.SecretsExplorer.Common;
using BlaBlaPrincess.SecretsExplorer.Data;

namespace BlaBlaPrincess.SecretsExplorer.Business
{
    public class UtromsSecretsExplorer : ISecretsExplorer
    {
        private SecretDirectory _processedDirectory;

        public void ExploreSecrets()
        {
            var source = "./";
            ExploreSecrets(source);
        }

        public void ExploreSecrets(string source)
        {
            _processedDirectory = new SecretDirectory("Utrom's secrets");
            ProcessDirectory(source);
        }

        private void RiseUp()
        {
            if (!_processedDirectory.IsRoot)
            {
                _processedDirectory = _processedDirectory.Parent;
            }
        }

        private string GetSecretName(string path)
        {
            var name = PathHelper.GetFileOrDirectoryName(path);
            UuidHelper.TryToRemoveUuids(name, out name);
            return name;
        }

        private void ProcessDirectory(string source)
        {
            var files = Directory.GetFiles(source);
            foreach (var filePath in files)
            {
                var name = GetSecretName(filePath);
                var weight = UtromsSecretSizeHelper.GetSecretSize(filePath);
                var secret = new SecretFile(name, weight);
                _processedDirectory.Children.Add(secret);
            }

            var dirs = Directory.GetDirectories(source);
            foreach (var dirPath in dirs)
            {
                var name = GetSecretName(dirPath);
                var dir = new SecretDirectory(name, _processedDirectory);
                _processedDirectory.Children.Add(dir);
                _processedDirectory = dir;
                ProcessDirectory(dirPath);
            }
            
            RiseUp();
        }
    }
}