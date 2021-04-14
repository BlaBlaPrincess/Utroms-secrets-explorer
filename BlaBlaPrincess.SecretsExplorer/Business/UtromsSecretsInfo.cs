using System.Collections.Generic;
using System.Linq;
using BlaBlaPrincess.SecretsExplorer.Common;

namespace BlaBlaPrincess.SecretsExplorer.Business
{
    public class UtromsSecretsInfo
    {
        public readonly List<string> Destinations;
        public readonly List<string> ZipFiles;
        public readonly string ExploredSecrets;
        public readonly string Source;

        public UtromsSecretsInfo(string exploredSecrets, string source)
        {
            ExploredSecrets = exploredSecrets;
            Source = source;
            
            Destinations = new List<string>();
            ZipFiles = new List<string>();
        }

        public override string ToString()
        {
            var result = $"{ExploredSecrets}\n\n" +
                         $"Source folder: {Source}";
            if (Destinations.Any())
            {
                result += $"\n{CollectionHelper.CollectionToString(Destinations, "Destination folder", "Destination folders")}";
            }
            if (ZipFiles.Any())
            {
                result += $"\n{CollectionHelper.CollectionToString(ZipFiles, "Zip file", "Zip files")}";
            }
            return result;
        }
    }
}