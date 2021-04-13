using System;
using System.Collections.Generic;
using System.Linq;

namespace BlaBlaPrincess.SecretsExplorer.Data
{
    public class SecretDirectory : ISecret
    {
        public readonly List<ISecret> Children = new List<ISecret>();
        public string Name { get; set; }
        public SecretDirectory Parent { get; }
        public int Weight => Children.Sum(secret => secret.Weight);
        public bool IsRoot => Parent is null;

        public int Depth
        {
            get
            {
                var depth = 0;
                var node = this;
                while (!node.IsRoot)
                {
                    depth++;
                    node = node.Parent;
                }
                return depth;
            }
        }

        public SecretDirectory(string name, SecretDirectory parent = null)
        {
            if (parent != null && parent.Equals(this))
            {
                throw new ArgumentException("Directory cannot be its own parent.");
            }
            Parent = parent;
            Name = name;
        }

        public bool Equals(SecretDirectory directory)
        {
            return Name == directory.Name &&
                   Children.SequenceEqual(directory.Children);
        }

        public override bool Equals(object obj)
        {
            return obj is SecretDirectory directory && Equals(directory);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Children, Parent);
        }

        public override string ToString()
        {
            var result = string.Empty;

            result += $"{Name} ({Weight}Kb)/\n";
            foreach (var secret in Children)
            {
                if (result[^1] != '\n')
                {
                    result += '\n';
                }
                for (int i = 0; i <= Depth; i++)
                {
                    result += @"    ";
                }
                result += $"|- {secret}";
            }

            return result;
        }
    }
}