using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FolderStructureExplorer.BL.Contracts;

namespace FolderStructureExplorer.BL
{
    public class DirectoryExplorer : IDirectoryExplorer
    {
        public IEnumerable<string> Explore(string directoryPath)
        {
            var foundFileSystemEntities = new List<string>();

            var researchDirectories = new Queue<string>();

            researchDirectories.Enqueue(directoryPath);

            while (researchDirectories.Any())
            {
                var currentDirectory = researchDirectories.Dequeue();

                var foundDirectories = Directory.EnumerateDirectories(currentDirectory).Select(d => new DirectoryInfo(d));

                foreach (var directory in foundDirectories)
                {
                    foundFileSystemEntities.Add(directory.Name);

                    researchDirectories.Enqueue(directory.FullName);
                }

                var foundFiles = Directory.EnumerateFiles(currentDirectory).Select(f => new FileInfo(f));

                foreach (var file in foundFiles)
                {
                    foundFileSystemEntities.Add(file.Name);
                }
            }

            return foundFileSystemEntities;
        }
    }
}