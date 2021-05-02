using System.Collections.Generic;
using System.Linq;

namespace BlaBlaPrincess.SecretsExplorer.Common
{
    public static class EnumerableHelper
    {
        public static string EnumerableToString(IEnumerable<string> enumerable, string singular, string plural)
        {
            var result = string.Empty;
            var collection = enumerable as string[] ?? enumerable.ToArray();
            switch (collection.Length)
            {
                case 0:
                    break;
                case 1:
                    result += $"{singular}: {collection[0]}";
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