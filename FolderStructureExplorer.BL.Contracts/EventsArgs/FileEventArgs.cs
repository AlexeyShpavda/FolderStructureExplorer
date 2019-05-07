using System.IO;

namespace FolderStructureExplorer.BL.Contracts.EventsArgs
{
    public class FileEventArgs : FurtherActionEventArgs
    {
        public FileInfo FileInfo { get; set; }
    }
}