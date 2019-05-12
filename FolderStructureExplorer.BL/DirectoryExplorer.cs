using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FolderStructureExplorer.BL.Contracts.Enums;
using FolderStructureExplorer.BL.Contracts.EventsArgs;
using FolderStructureExplorer.BL.Contracts.Interfaces;

namespace FolderStructureExplorer.BL
{
    public class DirectoryExplorer : IDirectoryExplorer
    {
        #region Events
        public event EventHandler<DirectoryEventArgs> NotifyAboutStartingOfExplore;

        public event EventHandler<DirectoryEventArgs> NotifyAboutFinishingOfExplore;

        public event EventHandler<FileEventArgs> NotifyThatFileWasFounded;

        public event EventHandler<DirectoryEventArgs> NotifyThatDirectoryWasFounded;

        public event EventHandler<FileEventArgs> NotifyThatFilePassedFilteringSuccessfully;

        public event EventHandler<DirectoryEventArgs> NotifyThatDirectoryPassedFilteringSuccessfully;

        public event EventHandler<FileEventArgs> NotifyThatFileNotFiltered;

        public event EventHandler<DirectoryEventArgs> NotifyThatDirectoryNotFiltered;
        #endregion

        private Predicate<FileInfo> FilePredicate { get; }

        private Predicate<DirectoryInfo> DirectoryPredicate { get; }

        public DirectoryExplorer(
            Predicate<FileInfo> filePredicate = null,
            Predicate<DirectoryInfo> directoryPredicate = null)
        {
            FilePredicate = filePredicate;
            DirectoryPredicate = directoryPredicate;
        }

        public IEnumerable<string> Explore(string directoryPath)
        {
            OnNotifyAboutStartingOfExplore(new DirectoryEventArgs
            {
                DirectoryInfo = new DirectoryInfo(directoryPath)
            });

            var researchDirectories = new Queue<string>();

            researchDirectories.Enqueue(directoryPath);

            while (researchDirectories.Any())
            {
                var currentDirectory = researchDirectories.Dequeue();

                var foundDirectories = Directory.EnumerateDirectories(currentDirectory).Select(d => new DirectoryInfo(d));

                foreach (var directory in foundDirectories)
                {
                    var directoryEventArgs = new DirectoryEventArgs { DirectoryInfo = directory };

                    OnNotifyThatDirectoryWasFounded(directoryEventArgs);

                    if (DirectoryPredicate != null && !DirectoryPredicate(directory))
                    {
                        OnNotifyThatDirectoryNotFiltered(directoryEventArgs);
                        continue;
                    }

                    OnNotifyThatDirectoryPassedFilteringSuccessfully(directoryEventArgs);

                    switch (directoryEventArgs.FurtherAction)
                    {
                        case FurtherAction.Skip:
                            continue;
                        case FurtherAction.Stop:
                            yield break;
                    }

                    yield return directory.Name;

                    researchDirectories.Enqueue(directory.FullName);
                }

                var foundFiles = Directory.EnumerateFiles(currentDirectory).Select(f => new FileInfo(f));

                foreach (var file in foundFiles)
                {
                    var fileEventArgs = new FileEventArgs {FileInfo = file};

                    OnNotifyThatFileWasFounded(fileEventArgs);

                    if (FilePredicate != null && !FilePredicate(file))
                    {
                        OnNotifyThatFileNotFiltered(fileEventArgs);
                        continue;
                    }

                    OnNotifyThatFilePassedFilteringSuccessfully(fileEventArgs);

                    switch (fileEventArgs.FurtherAction)
                    {
                        case FurtherAction.Skip:
                            continue;
                        case FurtherAction.Stop:
                            yield break;
                    }

                    yield return file.Name;
                }
            }

            OnNotifyAboutFinishingOfExplore(new DirectoryEventArgs
            {
                DirectoryInfo = new DirectoryInfo(directoryPath)
            });
        }

        #region OnEvents
        protected virtual void OnNotifyAboutStartingOfExplore(DirectoryEventArgs e)
        {
            NotifyAboutStartingOfExplore?.Invoke(this, e);
        }

        protected virtual void OnNotifyAboutFinishingOfExplore(DirectoryEventArgs e)
        {
            NotifyAboutFinishingOfExplore?.Invoke(this, e);
        }

        protected virtual void OnNotifyThatFileWasFounded(FileEventArgs e)
        {
            NotifyThatFileWasFounded?.Invoke(this, e);
        }

        protected virtual void OnNotifyThatDirectoryWasFounded(DirectoryEventArgs e)
        {
            NotifyThatDirectoryWasFounded?.Invoke(this, e);
        }

        protected virtual void OnNotifyThatFilePassedFilteringSuccessfully(FileEventArgs e)
        {
            NotifyThatFilePassedFilteringSuccessfully?.Invoke(this, e);
        }

        protected virtual void OnNotifyThatDirectoryPassedFilteringSuccessfully(DirectoryEventArgs e)
        {
            NotifyThatDirectoryPassedFilteringSuccessfully?.Invoke(this, e);
        }

        protected virtual void OnNotifyThatFileNotFiltered(FileEventArgs e)
        {
            NotifyThatFileNotFiltered?.Invoke(this, e);
        }

        protected virtual void OnNotifyThatDirectoryNotFiltered(DirectoryEventArgs e)
        {
            NotifyThatDirectoryNotFiltered?.Invoke(this, e);
        }
        #endregion
    }
}