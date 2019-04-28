﻿using System;
using FolderStructureExplorer.BL;

namespace FolderStructureExplorer.PL.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var directoryExplorer = new DirectoryExplorer();

            Console.WriteLine("Enter start directory path, please-- > ");
            var startDirectory = Console.ReadLine();

            directoryExplorer.Explore(startDirectory);

            Console.ReadKey();
        }
    }
}