using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FolderStructureExplorer.BL.Contracts.Interfaces;

namespace FolderStructureExplorer.BL
{
    public class DirectoryExplorer : IDirectoryExplorer
    {
        public IEnumerable<string> Explore(string directoryPath)
        {
            var researchDirectories = new Queue<string>();

            researchDirectories.Enqueue(directoryPath);

            while (researchDirectories.Any())
            {
                var currentDirectory = researchDirectories.Dequeue();

                var foundDirectories = Directory.EnumerateDirectories(currentDirectory).Select(d => new DirectoryInfo(d));

                foreach (var directory in foundDirectories)
                {
                    yield return directory.Name;

                    researchDirectories.Enqueue(directory.FullName);
                }

                var foundFiles = Directory.EnumerateFiles(currentDirectory).Select(f => new FileInfo(f));

                foreach (var file in foundFiles)
                {
                    yield return file.Name;
                }
            }
        }
    }
}