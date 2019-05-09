using System;
using System.Text;
using FolderStructureExplorer.BL;

namespace FolderStructureExplorer.PL.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                var directoryExplorer = new DirectoryExplorer();

                var notification = new Notification(directoryExplorer);

                notification.SubscribeToEvents();

                Console.WriteLine("Enter start directory path, please-- > ");
                //var startDirectory = Console.ReadLine();
                const string startDirectory = @"D:\AAA";

                foreach (var entity in directoryExplorer.Explore(startDirectory))
                {
                    Console.WriteLine(entity);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
