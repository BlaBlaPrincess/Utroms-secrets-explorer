using System;
using System.Collections.Generic;
using System.Linq;
using BlaBlaPrincess.SecretsExplorer.Common;

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

        private bool SecretExists(ISecret secret, IEnumerable<ISecret> collection)
        {
            var secrets = collection.Where(s => s.Equals(secret));
            var enumerable = secrets as SecretDirectory[] ?? secrets.ToArray();
            return enumerable.Any();
        }

        public void RemoveDuplicates()
        {
            var uniqueSecrets = new List<ISecret>();
            var duplicates = new List<ISecret>();
            foreach (var secret in Children)
            {
                if (!SecretExists(secret, uniqueSecrets))
                {
                    uniqueSecrets.Add(secret);
                }
                else
                {
                    duplicates.Add(secret);
                }
            }
            foreach (var duplicate in duplicates)
            {
                Children.Remove(duplicate);
            }
        }

        public void SetUniqueNames()
        {
            var weightDistributions = new Dictionary<string, List<ISecret>>();
            foreach (var secret in Children)
            {
                if (weightDistributions.TryGetValue(secret.Name, out var weights))
                {
                    weights.Add(secret);
                }
                else
                {
                    weightDistributions.Add(secret.Name, new List<ISecret>{secret});
                }
            }
            foreach (var collection in weightDistributions.Select(pair => pair.Value))
            {
                collection.Sort((x, y) => y.Weight.CompareTo(x.Weight));
                var id = 0;
                foreach (var secret in collection)
                {
                    secret.Name = PathHelper.AddIdentifier(secret.Name, id++);
                }
            }
        }
        
        public bool Equals(SecretDirectory directory)
        {
            return Name == directory.Name &&
                   EnumerableHelper.ScrambledEquals(Children, directory.Children);
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