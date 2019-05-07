using System.IO;

namespace FolderStructureExplorer.BL.Contracts.EventsArgs
{
    public class DirectoryEventArgs : FurtherActionEventArgs
    {
        public DirectoryInfo DirectoryInfo { get; set; }
    }
}