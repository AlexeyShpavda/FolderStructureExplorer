using System;
using FolderStructureExplorer.BL.Contracts.Enums;

namespace FolderStructureExplorer.BL.Contracts.EventsArgs
{
    public class FurtherActionEventArgs : EventArgs
    {
        public FurtherAction FurtherAction { get; set; }
    }
}