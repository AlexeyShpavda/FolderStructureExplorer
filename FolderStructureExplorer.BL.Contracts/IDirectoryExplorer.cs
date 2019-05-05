using System.Collections.Generic;

namespace FolderStructureExplorer.BL.Contracts
{
    public interface IDirectoryExplorer
    {
        IEnumerable<string> Explore(string directoryPath);
    }
}