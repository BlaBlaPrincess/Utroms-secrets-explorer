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

        public static bool ScrambledEquals<T>(IEnumerable<T> enumerable1, IEnumerable<T> enumerable2)
        {
            var collection1 = enumerable1.ToList();
            var collection2 = enumerable2.ToList();
            
            if (collection1.Count != collection2.Count) return false;

            while (collection1.Count != 0)
            {
                var matchFound = false;
                var index = 0;
                foreach (var item in collection2)
                {
                    if (collection1[0].Equals(item))
                    {
                        matchFound = true;
                        break;
                    }
                    
                    index++;
                }

                if (matchFound)
                {
                    collection1.RemoveAt(0);
                    collection2.RemoveAt(index);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}