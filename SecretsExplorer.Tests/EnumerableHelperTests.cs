using BlaBlaPrincess.SecretsExplorer.Common;
using NUnit.Framework;

namespace BlaBlaPrincess.SecretsExplorer.Tests
{
    internal class EnumerableHelperTests
    {
        #region HashMapEquals Tests

        [Test]
        public void HashMapEquals_IdenticalCollections_True()
        {
            var enumerable1 = new [] { 1,2 };
            var enumerable2 = new [] { 1,2 };

            var isHashMapEquals = EnumerableHelper.HashMapEquals(enumerable1, enumerable2);
            
            Assert.IsTrue(isHashMapEquals);
        }
        
        [Test]
        public void HashMapEquals_ScrambledCollections_True()
        {
            var enumerable1 = new [] { 1,2 };
            var enumerable2 = new [] { 2,1 };

            var isHashMapEquals = EnumerableHelper.HashMapEquals(enumerable1, enumerable2);
            
            Assert.IsTrue(isHashMapEquals);
        }
        
        [Test]
        public void HashMapEquals_DifferentCollections_False()
        {
            var enumerable1 = new [] { 1,1 };
            var enumerable2 = new [] { 1,2 };

            var isHashMapEquals = EnumerableHelper.HashMapEquals(enumerable1, enumerable2);
            
            Assert.IsFalse(isHashMapEquals);
        }
        
        [Test]
        public void HashMapEquals_EmptyCollections_True()
        {
            var enumerable1 = new int[] { };
            var enumerable2 = new int[] { };

            var isHashMapEquals = EnumerableHelper.HashMapEquals(enumerable1, enumerable2);
            
            Assert.IsTrue(isHashMapEquals);
        }

        #endregion

        #region ScrambledEquals Tests

        [Test]
        public void ScrambledEquals_IdenticalCollections_True()
        {
            var enumerable1 = new [] { 1,2 };
            var enumerable2 = new [] { 1,2 };

            var isScrambledEquals = EnumerableHelper.ScrambledEquals(enumerable1, enumerable2);
            
            Assert.IsTrue(isScrambledEquals);
        }
        
        [Test]
        public void ScrambledEquals_ScrambledCollections_True()
        {
            var enumerable1 = new [] { 1,2 };
            var enumerable2 = new [] { 2,1 };

            var isScrambledEquals = EnumerableHelper.ScrambledEquals(enumerable1, enumerable2);
            
            Assert.IsTrue(isScrambledEquals);
        }
        
        [Test]
        public void ScrambledEquals_DifferentCollections_False()
        {
            var enumerable1 = new [] { 1,1 };
            var enumerable2 = new [] { 1,2 };

            var isScrambledEquals = EnumerableHelper.ScrambledEquals(enumerable1, enumerable2);
            
            Assert.IsFalse(isScrambledEquals);
        }
        
        [Test]
        public void ScrambledEquals_EmptyCollections_True()
        {
            var enumerable1 = new int[] { };
            var enumerable2 = new int[] { };

            var isScrambledEquals = EnumerableHelper.ScrambledEquals(enumerable1, enumerable2);
            
            Assert.IsTrue(isScrambledEquals);
        }

        #endregion
    }
}