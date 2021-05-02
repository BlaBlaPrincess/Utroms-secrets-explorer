using System;
using System.IO;
using System.IO.Compression;
using BlaBlaPrincess.SecretsExplorer.Common;
using BlaBlaPrincess.SecretsExplorer.Data;

namespace BlaBlaPrincess.SecretsExplorer.Business
{
    public class UtromsSecretsExplorer : ISecretsExplorer
    {
        public bool SecretsExplored => _processedDirectory is null;
        
        private SecretDirectory _processedDirectory;
        private UtromsSecretsInfo _info;

        public UtromsSecretsInfo GetInfo()
        {
            return SecretsExplored
                ? _info
                : throw new InvalidOperationException(
                    "ExploreSecrets method must be called before getting the information.");
        }
        
        public void ExploreSecrets()
        {
            var source = "./";
            ExploreSecrets(source);
        }

        public void ExploreSecrets(string source)
        {
            _processedDirectory = new SecretDirectory("Utrom's secrets");
            ProcessDirectory(source);

            _info = new UtromsSecretsInfo(_processedDirectory.ToString(),
                PathHelper.UnifySeparator(Path.TrimEndingDirectorySeparator(source)));
        }

        public void ExploreSecrets(string source, string destination, bool zip = false)
        {
            ExploreSecrets(source);
            if (!zip)
            {
                SaveSecrets(destination);
            }
            else
            {
                ZipSecrets(destination);
            }
        }
        
        private void SaveSecrets(string destination)
        {
            SaveDirectory(destination);
            _info.Destinations.Add(PathHelper.UnifySeparator(Path.Combine(destination, _processedDirectory.Name)));
        }

        private void ZipSecrets(string destination)
        {
            Directory.CreateDirectory(destination);
            var path = Path.Combine(destination, _processedDirectory.Name) + ".zip";
            using var zipToOpen = new FileStream(path, FileMode.Create);
            using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);
            ZipDirectory(archive, string.Empty);
            _info.ZipFiles.Add(PathHelper.UnifySeparator(path));
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
            
            _processedDirectory.RemoveDuplicates();
            RiseUp();
        }

        private void SaveDirectory(string destination)
        {
            var path = Path.Combine(destination, _processedDirectory.Name);
            Directory.CreateDirectory(path);
            foreach (var secret in _processedDirectory.Children)
            {
                if (secret is SecretDirectory dir)
                {
                    _processedDirectory = dir;
                    SaveDirectory(path);
                }
                else
                {
                    var file = Path.Combine(path, secret.Name);
                    using var sw = new StreamWriter(file);
                    sw.Write($"{secret.Weight}Kb");
                }
            }
            RiseUp();
        }

        private void ZipDirectory(ZipArchive archive, string destinationRelativeToArchive)
        {
            foreach (var secret in _processedDirectory.Children)
            {
                if (secret is SecretDirectory dir)
                {
                    _processedDirectory = dir;
                    var dirName = $"{Path.Combine(destinationRelativeToArchive, dir.Name)}/";
                    archive.CreateEntry(dirName);
                    ZipDirectory(archive, dirName);
                }
                else
                {
                    var file = Path.Combine(destinationRelativeToArchive, secret.Name);
                    var entry = archive.CreateEntry(file);
                    using var sw = new StreamWriter(entry.Open());
                    sw.Write($"{secret.Weight}Kb");
                }
            }
            RiseUp();
        }
    }
}