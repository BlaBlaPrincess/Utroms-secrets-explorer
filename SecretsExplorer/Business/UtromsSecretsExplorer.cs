using System;
using System.IO;
using System.IO.Compression;
using BlaBlaPrincess.SecretsExplorer.Common;
using BlaBlaPrincess.SecretsExplorer.Data;

namespace BlaBlaPrincess.SecretsExplorer.Business
{
    public class UtromsSecretsExplorer : ISecretsExplorer
    {
        public bool SecretsExplored => _currentDirectory is not null;
        
        private SecretDirectory _currentDirectory;
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
            _currentDirectory = new SecretDirectory("Utrom's secrets");
            ExploreCurrentDirectory(source);

            _info = new UtromsSecretsInfo(_currentDirectory.ToString(),
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
            SaveCurrentDirectory(destination);
            _info.Destinations.Add(PathHelper.UnifySeparator(Path.Combine(destination, _currentDirectory.Name)));
        }

        private void ZipSecrets(string destination)
        {
            Directory.CreateDirectory(destination);
            var path = Path.Combine(destination, _currentDirectory.Name) + ".zip";
            using var zipToOpen = new FileStream(path, FileMode.Create);
            using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);
            ZipCurrentDirectory(archive, string.Empty);
            _info.ZipFiles.Add(PathHelper.UnifySeparator(path));
        }
        
        private void RiseUp()
        {
            if (!_currentDirectory.IsRoot)
            {
                _currentDirectory = _currentDirectory.Parent;
            }
        }

        private string GetSecretName(string path)
        {
            var name = PathHelper.GetFileOrDirectoryName(path);
            UuidHelper.TryToRemoveUuid(name, out name);
            return name;
        }

        private void ExploreCurrentDirectory(string source)
        {
            var files = Directory.GetFiles(source);
            foreach (var filePath in files)
            {
                var name = GetSecretName(filePath);
                var weight = UtromsSecretSizeHelper.GetSecretSize(filePath);
                var secret = new SecretFile(name, weight);
                _currentDirectory.Children.Add(secret);
            }

            var dirs = Directory.GetDirectories(source);
            foreach (var dirPath in dirs)
            {
                var name = GetSecretName(dirPath);
                var dir = new SecretDirectory(name, _currentDirectory);
                _currentDirectory.Children.Add(dir);
                _currentDirectory = dir;
                ExploreCurrentDirectory(dirPath);
            }
            
            _currentDirectory.RemoveDuplicates();
            _currentDirectory.SetUniqueNames();
            RiseUp();
        }

        private void SaveCurrentDirectory(string destination)
        {
            var path = Path.Combine(destination, _currentDirectory.Name);
            Directory.CreateDirectory(path);
            foreach (var secret in _currentDirectory.Children)
            {
                if (secret is SecretDirectory dir)
                {
                    _currentDirectory = dir;
                    SaveCurrentDirectory(path);
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

        private void ZipCurrentDirectory(ZipArchive archive, string destinationRelativeToArchive)
        {
            foreach (var secret in _currentDirectory.Children)
            {
                if (secret is SecretDirectory dir)
                {
                    _currentDirectory = dir;
                    var dirName = $"{Path.Combine(destinationRelativeToArchive, dir.Name)}/";
                    archive.CreateEntry(dirName);
                    ZipCurrentDirectory(archive, dirName);
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