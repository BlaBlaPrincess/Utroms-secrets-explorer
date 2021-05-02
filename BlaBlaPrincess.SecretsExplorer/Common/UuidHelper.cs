using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlaBlaPrincess.SecretsExplorer.Common
{
    public static class UuidHelper
    {
        public static bool TryToRemoveUuid(string input, out string result)
        {
            result = input;
            var pattern = @" [0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}";
            var match = Regex.Match(input, pattern);
            if (match.Success)
            {
                result = match.Value;
                return true;
            }
            return false;
        }
    }
}