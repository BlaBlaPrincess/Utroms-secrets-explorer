using System.Collections.Generic;
using System.Linq;

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

        private string ListToString(List<string> list, string singular, string plural)
        {
            var result = string.Empty;
            switch (list.Count)
            {
                case 0:
                    break;
                case 1:
                    result += $"{singular}: {list[0]}";
                    break;
                default:
                    result += $"{plural}:";
                    foreach (var s in list)
                    {
                        result += $"\n    {s}";
                    }
                    break;
            }
            return result;
        }

        public override string ToString()
        {
            var result = $"{ExploredSecrets}\n\n" +
                         $"Source folder: {Source}";
            if (Destinations.Any())
            {
                result += $"\n{ListToString(Destinations, "Destination folder", "Destination folders")}";
            }
            if (ZipFiles.Any())
            {
                result += $"\n{ListToString(ZipFiles, "Zip file", "Zip files")}";
            }
            return result;
        }
    }
}