using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using FolderStructureExplorer.BL.Contracts.Interfaces;
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
            var treeFolders = new TreeFolders(_testDirectoryPath);
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

    internal class TreeFolders
    {
        private static Stack<string> _folders;

        public TreeFolders(string path)
        {
            _folders = new Stack<string>();
            _folders.Push(path);
        }

        public void Create(int numberFolders)
        {
            var extensionList = new List<string> {".txt", ".pdf", ".xlsm"};
            if (numberFolders == 0)
            {
                return;
            }

            var folder = _folders.Pop();

            for (var i = 1; i <= numberFolders; i++)
            {
                var path = $@"{folder}\{i}";

                Directory.CreateDirectory(path);
                foreach (var extension in extensionList)
                {
                    File.Create($@"{path}\{DateTime.Now:yyyy.MM.dd_HH-mm-ss}{extension}");
                }
                _folders.Push(path);
                Create(numberFolders - 1);
            }
        }
    }
}