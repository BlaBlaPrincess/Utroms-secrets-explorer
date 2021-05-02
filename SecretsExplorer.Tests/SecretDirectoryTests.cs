using BlaBlaPrincess.SecretsExplorer.Data;
using Moq;
using NUnit.Framework;

namespace BlaBlaPrincess.SecretsExplorer.Tests
{
    internal class SecretDirectoryTests
    {
        #region Setup

        private SecretDirectory _directory;
        private SecretDirectory _expected;

        private Mock<ISecret> _mockData257Kb;
        private Mock<ISecret> _mockData256Kb;
        private Mock<ISecret> _mockData25Kb;
        
        [SetUp]
        public void SetupMocks()
        {
            _mockData257Kb = new Mock<ISecret>();
            _mockData257Kb.Setup(f => f.Name).Returns("data.plan");
            _mockData257Kb.Setup(f => f.Weight).Returns(257);
            
            _mockData256Kb = new Mock<ISecret>();
            _mockData256Kb.Setup(f => f.Name).Returns("data.plan");
            _mockData256Kb.Setup(f => f.Weight).Returns(256);
            
            _mockData25Kb = new Mock<ISecret>();
            _mockData25Kb.Setup(f => f.Name).Returns("data.plan");
            _mockData25Kb.Setup(f => f.Weight).Returns(25);
        }
        
        public void SetupSimilarUnorderedDirectories()
        {
            var root = new SecretDirectory("root");
            
            _directory = new SecretDirectory("data");
            _directory.Children.AddRange( new ISecret[]
            {
                new SecretFile("1", 0),
                new SecretFile("2", 0),
                new SecretDirectory("A", root),
                new SecretDirectory("A", root)
                {
                    Children =
                    {
                        new SecretFile("I", 0),
                        new SecretFile("II", 0)
                    }
                }
            });
            
            _expected = new SecretDirectory("data");
            _expected.Children.AddRange( new ISecret[]
            {
                new SecretDirectory("A", root),
                new SecretDirectory("A", root)
                {
                    Children =
                    {
                        new SecretFile("II", 0),
                        new SecretFile("I", 0)
                    }
                },
                new SecretFile("1", 0),
                new SecretFile("2", 0)
            });
        }

        public void SetupSameDirectories()
        {
            _directory = new SecretDirectory("data");
            _expected = _directory;
        }

        #endregion

        #region GetHashCode Tests

        [Test]
        public void GetHashCode_SimilarUnordered_NotEqual()
        {
            SetupSimilarUnorderedDirectories();

            var hash1 = _directory.GetHashCode();
            var hash2 = _expected.GetHashCode();
            
            Assert.AreNotEqual(hash1, hash2);
        }
        
        [Test]
        public void GetHashCode_SameUnordered_Equal()
        {
            SetupSameDirectories();

            var hash1 = _directory.GetHashCode();
            var hash2 = _expected.GetHashCode();
            
            Assert.AreEqual(hash1, hash2);
        }

        #endregion
        
        #region Equals Tests

        [Test]
        public void Equals_SimilarUnordered_Equal()
        {
            SetupSimilarUnorderedDirectories();

            Assert.AreEqual(_directory, _expected);
        }
        
        [Test]
        public void Equals_SameUnordered_Equal()
        {
            SetupSameDirectories();
            
            Assert.AreEqual(_directory, _expected);
        }

        #endregion

        #region RemoveDuplicates Tests

        [Test]
        public void RemoveDuplicates_Set1_Equal()
        {
            var dir = new SecretDirectory("root");
            dir.Children.AddRange(new []
            {
                _mockData257Kb.Object,
                _mockData257Kb.Object,
                _mockData256Kb.Object,
                _mockData256Kb.Object,
                _mockData25Kb.Object
            });
            var expected = new SecretDirectory("root");
            expected.Children.AddRange(new []
            {
                _mockData25Kb.Object,
                _mockData256Kb.Object,
                _mockData257Kb.Object
            });

            dir.RemoveDuplicates();
            
            Assert.AreEqual(expected, dir);
        }

        [Test]
        public void RemoveDuplicates_Set2_Equal()
        {
            var dir = new SecretDirectory("root");
            dir.Children.AddRange(new ISecret[]
            {
                new SecretDirectory("data", dir) {Children =
                {
                    _mockData257Kb.Object,
                    _mockData256Kb.Object
                }},
                new SecretDirectory("data", dir) {Children =
                {
                    _mockData257Kb.Object,
                    _mockData256Kb.Object
                }},
                new SecretDirectory("data", dir) {Children =
                {
                    _mockData257Kb.Object,
                    _mockData256Kb.Object,
                    _mockData25Kb.Object
                }}
            });
            var expected = new SecretDirectory("root");
            expected.Children.AddRange(new ISecret[]
            {
                new SecretDirectory("data", dir) {Children =
                {
                    _mockData257Kb.Object,
                    _mockData256Kb.Object
                }},
                new SecretDirectory("data", dir) {Children =
                {
                    _mockData257Kb.Object,
                    _mockData256Kb.Object,
                    _mockData25Kb.Object
                }}
            });
            
            dir.RemoveDuplicates();
            
            Assert.AreEqual(expected, dir);
        }

        #endregion

        #region SetUniqueNames Tests

        [Test]
        public void SetUniqueNames_Set1_Equal()
        {
            var dir = new SecretDirectory("root");
            dir.Children.AddRange(new []
            {
                _mockData257Kb.Object,
                _mockData256Kb.Object,
                _mockData25Kb.Object
            });
            
            var expected = new SecretDirectory("root");
            
            var renamedData25Kb = _mockData25Kb.Object;
            renamedData25Kb.Name = "data (2).plan";
            
            var renamedData256Kb = _mockData256Kb.Object;
            renamedData256Kb.Name = "data (1).plan";
            
            expected.Children.AddRange(new []
            {
                renamedData25Kb,
                renamedData256Kb,
                _mockData257Kb.Object
            });

            dir.SetUniqueNames();
            
            Assert.AreEqual(expected, dir);
        }
        
        [Test]
        public void SetUniqueNames_Set2_Equal()
        {
            var dir = new SecretDirectory("root");
            dir.Children.AddRange(new []
            {
                new SecretDirectory("data", dir) {Children =
                {
                    _mockData25Kb.Object
                }},
                new SecretDirectory("data", dir) {
                Children =
                {
                    _mockData256Kb.Object
                }}
            });
            var expected = new SecretDirectory("root");
            expected.Children.AddRange(new []
            {
                new SecretDirectory("data", dir) {Children =
                {
                    _mockData256Kb.Object
                }},
                new SecretDirectory("data (1)", dir) {Children =
                {
                    _mockData25Kb.Object
                }}
            });

            dir.SetUniqueNames();
            
            Assert.AreEqual(expected, dir);
        }

        #endregion
    }
}