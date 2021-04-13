using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlaBlaPrincess.SecretsExplorer.Common
{
    public static class UuidHelper
    {
        public static bool TryToRemoveUuids(string input, out string result)
        {
            result = input;
            var pattern = @" [0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}";
            var matches = Regex.Matches(input, pattern, RegexOptions.None);
            if (!matches.Any())
            {
                return false;
            }
            foreach (Match match in matches)
            {
                result = result.Replace(match.Value, string.Empty);
            }
            return true;
        }
    }
}