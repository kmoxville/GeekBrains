using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManager
{
    public class FileManagerModel : IFileManagerModel
    {
        private DirectoryInfo _currentDirectory;

        public FileManagerModel(DirectoryInfo currentDirectory)
        {
            CurrentDirectory = currentDirectory;
        }

        public FileManagerModel(string path)
        {
            try 
            {
                CurrentDirectory = new DirectoryInfo(path);
            }
            catch
            {
                CurrentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            }
        }

        public FileManagerModel()
            : this(new DirectoryInfo(Environment.CurrentDirectory))
        {
        
        }

        public DirectoryInfo CurrentDirectory 
        { 
            get => _currentDirectory; 
            set 
            {
                if (value == null) throw new ArgumentNullException("CurrentDirectory");
                if (!value.Exists) throw new DirectoryNotFoundException();

                _currentDirectory = value;
            } 
        }

        public FileSystemInfo[] Content => _currentDirectory.GetFileSystemInfos();

        public void ChangeDirectory(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("path");

            switch (path)
            {
                case "..":
                    CurrentDirectory = CurrentDirectory.Parent;
                    break;
                case ".":
                    CurrentDirectory = CurrentDirectory;
                    break;
                default:
                    if (!Path.IsPathRooted(path))
                        CurrentDirectory = new DirectoryInfo(Path.Combine(CurrentDirectory.FullName, path));
                    else
                        CurrentDirectory = new DirectoryInfo(path);

                    break;
            }
        }

        public void ExecuteCommand(ICommand command, CommandlineOptions options)
        {
            command.Execute(this, options);
        }
    }
}
