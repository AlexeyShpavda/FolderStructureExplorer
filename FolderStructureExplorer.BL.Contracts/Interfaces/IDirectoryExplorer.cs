using System.Collections.Generic;

namespace FolderStructureExplorer.BL.Contracts.Interfaces
{
    public interface IDirectoryExplorer
    {
        IEnumerable<string> Explore(string directoryPath);
    }
}