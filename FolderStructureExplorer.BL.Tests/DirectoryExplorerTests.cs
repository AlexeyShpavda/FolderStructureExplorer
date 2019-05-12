using System;
using System.IO;
using System.Linq;
using FolderStructureExplorer.BL.Tests.Helpers;
using NUnit.Framework;

namespace FolderStructureExplorer.BL.Tests
{
    [TestFixture]
    public class DirectoryExplorerTests
    {
        private readonly string _testDirectoryPath = $@"{AppDomain.CurrentDomain.BaseDirectory}TestDirectory";

        private const int StructureDepth = 3;

        [OneTimeSetUp]
        public void Initialize()
        {
            if (Directory.Exists(_testDirectoryPath))
            {
                return;
            }

            Directory.CreateDirectory(_testDirectoryPath);
            var treeFolders = new FolderTreeCreator(_testDirectoryPath);
            treeFolders.Create(StructureDepth);
        }

        [Test]
        public void ResearchDirectory_PassedCorrectDirectoryPathWithoutPredicate_GetSeveralFiles()
        {
            var directoryExplorer = new DirectoryExplorer();

            Assert.AreEqual(directoryExplorer.Explore(_testDirectoryPath).Count(), 60);
        }

        [Test]
        [TestCase(".txt", 30)]
        [TestCase(".xlsm", 30)]
        [TestCase(".pdf", 30)]
        public void ResearchDirectory_PassedCorrectDirectoryPathWithSpecificExtension_GetSeveralFiles(string extension, int expected)
        {
            var directoryExplorer = new DirectoryExplorer(
                fileInfo => fileInfo.Extension == extension);

            Assert.AreEqual(directoryExplorer.Explore(_testDirectoryPath).Count(), 30);
        }

        [Test]
        [TestCase("1", 12)]
        [TestCase("2", 8)]
        [TestCase("3", 4)]
        public void ResearchDirectory_PassedCorrectDirectoryPathWithSpecificFolderNames_GetSeveralFiles(string folderName, int expected)
        {
            var directoryExplorer = new DirectoryExplorer(
                null, directoryInfo => directoryInfo.Name == folderName);

            Assert.AreEqual(directoryExplorer.Explore(_testDirectoryPath).Count(), expected);
        }
    }
}