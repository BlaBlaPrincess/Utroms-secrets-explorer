﻿using System;
using System.IO;

namespace BlaBlaPrincess.SecretsExplorer.Common
{
    public static class UtromsSecretSizeHelper
    {
        public static int GetSecretSize(string path)
        {
            var sWeight = File.ReadAllText(path).Replace("Kb", string.Empty);
            try
            {
                return int.Parse(sWeight);
            }
            catch (FormatException)
            {
                throw new FormatException("An unexpected secret format.");
            }
        }

        public static bool TryToGetSecretSize(string path, out int weight)
        {
            var sWeight = File.ReadAllText(path).Replace("Kb", string.Empty);
            return int.TryParse(sWeight, out weight);
        }
    }
}