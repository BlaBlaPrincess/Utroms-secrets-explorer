using System;

namespace BlaBlaPrincess.SecretsExplorer.Data
{
    public class SecretFile : ISecret
    {
        public string Name { get; set; }
        public int Weight { get; }

        public SecretFile(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }

        public bool Equals(SecretFile file)
        {
            return Name == file.Name &&
                   Weight == file.Weight;
        }
        
        public override bool Equals(object obj)
        {
            return obj is SecretFile file && Equals(file);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Weight);
        }

        public override string ToString()
        {
            return $"{Name} {Weight}Kb";
        }
    }
}