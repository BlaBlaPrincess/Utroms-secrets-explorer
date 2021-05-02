using System.Collections.Generic;
using System.Linq;

namespace BlaBlaPrincess.SecretsExplorer.Common
{
    public static class CollectionHelper
    {
        public static string CollectionToString(ICollection<string> collection, string singular, string plural)
        {
            var result = string.Empty;
            switch (collection.Count)
            {
                case 0:
                    break;
                case 1:
                    result += $"{singular}: {collection.First()}";
                    break;
                default:
                    result += $"{plural}:";
                    foreach (var s in collection)
                    {
                        result += $"\n    {s}";
                    }
                    break;
            }
            return result;
        }
    }
}