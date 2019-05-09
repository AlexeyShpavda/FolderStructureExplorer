using FolderStructureExplorer.BL.Contracts.Enums;
using FolderStructureExplorer.BL.Contracts.EventsArgs;
using FolderStructureExplorer.BL.Contracts.Interfaces;

namespace FolderStructureExplorer.PL.ConsoleApp
{
    public class Notification
    {
        private IDirectoryExplorer DirectoryExplorer { get; }

        public Notification(IDirectoryExplorer directoryExplorer)
        {
            DirectoryExplorer = directoryExplorer;
        }

        public void SubscribeToEvents()
        {
            DirectoryExplorer.NotifyAboutStartingOfExplore += NotifyAboutResearchStart;
            DirectoryExplorer.NotifyAboutFinishingOfExplore += NotifyAboutResearchFinish;
            DirectoryExplorer.NotifyThatDirectoryWasFounded += NotifyThatDirectoryWasFounded;
            DirectoryExplorer.NotifyThatFileWasFounded += NotifyThatFileWasFounded;
            DirectoryExplorer.NotifyThatDirectoryPassedFilteringSuccessfully +=
                NotifyThatDirectoryPassedFilteringSuccessfully;
            DirectoryExplorer.NotifyThatFilePassedFilteringSuccessfully +=
                NotifyThatFilePassedFilteringSuccessfully;
            DirectoryExplorer.NotifyThatDirectoryNotFiltered += NotifyThatDirectoryNotFiltered;
            DirectoryExplorer.NotifyThatFileNotFiltered += NotifyThatFileNotFiltered;
        }

        private void UnsubscribeFromEvents()
        {
            DirectoryExplorer.NotifyAboutStartingOfExplore -= NotifyAboutResearchStart;
            DirectoryExplorer.NotifyAboutFinishingOfExplore -= NotifyAboutResearchFinish;
            DirectoryExplorer.NotifyThatDirectoryWasFounded -= NotifyThatDirectoryWasFounded;
            DirectoryExplorer.NotifyThatFileWasFounded -= NotifyThatFileWasFounded;
            DirectoryExplorer.NotifyThatDirectoryPassedFilteringSuccessfully -=
                NotifyThatDirectoryPassedFilteringSuccessfully;
            DirectoryExplorer.NotifyThatFilePassedFilteringSuccessfully -=
                NotifyThatFilePassedFilteringSuccessfully;
            DirectoryExplorer.NotifyThatDirectoryNotFiltered -= NotifyThatDirectoryNotFiltered;
            DirectoryExplorer.NotifyThatFileNotFiltered -= NotifyThatFileNotFiltered;
        }

        private void NotifyAboutResearchStart(object sender, DirectoryEventArgs e)
        {
            System.Console.WriteLine($"{e.DirectoryInfo.Name} directory research was stated");
        }

        private void NotifyAboutResearchFinish(object sender, DirectoryEventArgs e)
        {
            System.Console.WriteLine($"{e.DirectoryInfo.Name} directory research was finished");
        }

        private void NotifyThatDirectoryWasFounded(object sender, DirectoryEventArgs e)
        {
            System.Console.WriteLine($"{e.DirectoryInfo.Name} directory was founded");
        }

        private void NotifyThatFileWasFounded(object sender, FileEventArgs e)
        {
            System.Console.WriteLine($"{e.FileInfo.Name} file was founded");
        }

        private void NotifyThatDirectoryPassedFilteringSuccessfully(object sender, DirectoryEventArgs e)
        {
            System.Console.WriteLine($"{e.DirectoryInfo.Name} directory passed filtering successfully");
            AskSkipOrNot(e);
        }

        private void NotifyThatFilePassedFilteringSuccessfully(object sender, FileEventArgs e)
        {
            System.Console.WriteLine($"{e.FileInfo.Name} file passed filtering successfully");
            AskSkipOrNot(e);
        }

        private void NotifyThatDirectoryNotFiltered(object sender, DirectoryEventArgs e)
        {
            System.Console.WriteLine($"{e.DirectoryInfo.Name} directory not filtered");
        }

        private void NotifyThatFileNotFiltered(object sender, FileEventArgs e)
        {
            System.Console.WriteLine($"{e.FileInfo.Name} file not filtered");
        }

        private static void AskSkipOrNot(FurtherActionEventArgs e)
        {
            System.Console.WriteLine("Enter add / skip / stop");
            var answer = System.Console.ReadLine()?.ToLower();

            while (answer != null 
                   && answer != "add" 
                   && answer != "skip" 
                   && answer != "stop")
            {
                System.Console.WriteLine("You need to enter add / skip / stop");
                answer = System.Console.ReadLine();
            }

            switch (answer)
            {
                case "skip":
                    e.FurtherAction = FurtherAction.Skip;
                    break;
                case "stop":
                    e.FurtherAction = FurtherAction.Stop;
                    break;
                default:
                    e.FurtherAction = FurtherAction.Add;
                    break;
            }
        }

        ~Notification()
        {
            UnsubscribeFromEvents();
        }

    }
}