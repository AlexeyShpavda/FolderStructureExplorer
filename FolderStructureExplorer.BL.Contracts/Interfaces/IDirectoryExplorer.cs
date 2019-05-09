using System;
using System.Collections.Generic;
using FolderStructureExplorer.BL.Contracts.EventsArgs;

namespace FolderStructureExplorer.BL.Contracts.Interfaces
{
    public interface IDirectoryExplorer
    {
        #region Events
        event EventHandler<DirectoryEventArgs> NotifyAboutStartingOfExplore;

        event EventHandler<DirectoryEventArgs> NotifyAboutFinishingOfExplore;

        event EventHandler<FileEventArgs> NotifyThatFileWasFounded;

        event EventHandler<DirectoryEventArgs> NotifyThatDirectoryWasFounded;

        event EventHandler<FileEventArgs> NotifyThatFilePassedFilteringSuccessfully;

        event EventHandler<DirectoryEventArgs> NotifyThatDirectoryPassedFilteringSuccessfully;

        event EventHandler<FileEventArgs> NotifyThatFileNotFiltered;

        event EventHandler<DirectoryEventArgs> NotifyThatDirectoryNotFiltered;
        #endregion

        IEnumerable<string> Explore(string directoryPath);
    }
}