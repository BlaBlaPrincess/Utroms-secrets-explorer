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
        
        public static bool HashMapEquals<T>(IEnumerable<T> enumerable1, IEnumerable<T> enumerable2)
        {
            var counter = new Dictionary<T, int>();
            
            foreach (var item in enumerable1)
            {
                if (counter.ContainsKey(item))
                {
                    counter[item]++;
                }
                else
                {
                    counter.Add(item, 1);
                }
            }
            
            foreach (var item in enumerable2)
            {
                if (counter.ContainsKey(item))
                {
                    counter[item]--;
                }
                else
                {
                    return false;
                }
            }
            
            return counter.Values.All(count => count == 0);
        }
    }
}