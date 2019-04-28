using System;
using System.IO;
using System.Linq;
using FolderStructureExplorer.BL.Contracts;

namespace FolderStructureExplorer.BL
{
    public class DirectoryExplorer : IDirectoryExplorer
    {
        public void Explore(string directoryPath)
        {
            var foundDirectories = Directory.EnumerateDirectories(directoryPath).Select(d => new DirectoryInfo(d));

            foreach (var directory in foundDirectories)
            {
                Explore(directory.FullName);
            }

            var foundFiles = Directory.EnumerateFiles(directoryPath).Select(f => new FileInfo(f));

            foreach (var file in foundFiles)
            {
                Console.WriteLine(file.Name);
            }
        }
    }
}