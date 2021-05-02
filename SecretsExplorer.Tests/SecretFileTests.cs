using BlaBlaPrincess.SecretsExplorer.Data;
using NUnit.Framework;

namespace BlaBlaPrincess.SecretsExplorer.Tests
{
    internal class SecretFileTests
    {
        [Test]
        public void GetHashCode_Same_Equal()
        {
            var file1 = new SecretFile("data", 100);
            var file2 = file1;

            var hash1 = file1.GetHashCode();
            var hash2 = file2.GetHashCode();

            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void GetHashCode_Similar_NotEqual()
        {
            var file1 = new SecretFile("data", 100);
            var file2 = new SecretFile("data", 100);

            var hash1 = file1.GetHashCode();
            var hash2 = file2.GetHashCode();

            Assert.AreNotEqual(hash1, hash2);
        }
        
        [Test]
        public void Equals_Same_Equal()
        {
            var file1 = new SecretFile("data", 100);
            var file2 = file1;

            Assert.AreEqual(file1, file2);
        }
        
        [Test]
        public void Equals_Similar_NotEqual()
        {
            var file1 = new SecretFile("data", 100);
            var file2 = new SecretFile("data", 100);

            Assert.AreEqual(file1, file2);
        }
    }
}