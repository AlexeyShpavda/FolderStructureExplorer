using System;
using System.Collections.Generic;
using System.IO;

namespace FolderStructureExplorer.BL.Tests.Helpers
{
    internal class FolderTreeCreator
    {
        private readonly Stack<string> _folders;

        public FolderTreeCreator(string path)
        {
            _folders = new Stack<string>();
            _folders.Push(path);
        }

        public void Create(int numberFolders)
        {
            var extensionList = new List<string> { ".txt", ".pdf", ".xlsm" };
            if (numberFolders == 0)
            {
                return;
            }

            var folder = _folders.Pop();

            for (var i = 1; i <= numberFolders; i++)
            {
                var path = $@"{folder}\{i}";

                Directory.CreateDirectory(path);
                foreach (var extension in extensionList)
                {
                    File.Create($@"{path}\{DateTime.Now:yyyy.MM.dd_HH-mm-ss}{extension}");
                }
                _folders.Push(path);
                Create(numberFolders - 1);
            }
        }
    }
}