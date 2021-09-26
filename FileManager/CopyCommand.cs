using CmdParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManager
{
    public sealed class CopyCommand : ICommand
    {
        private string _pathFrom;
        private string _pathTo;
        private bool _forced;
        private IFileManagerModel _fileManagerModel;

        public void Execute(IFileManagerModel fileManagerModel, CommandlineOptions cmdOptions)
        {
            var options = cmdOptions as CopyCommandOptions;

            _pathFrom = options.From;
            _pathTo = options.To;
            _forced = options.Overwrite;

            _fileManagerModel = fileManagerModel;
            var fis = fileManagerModel.Content;

            FileSystemInfo fromFI = fis.First(fi => fi.Name == _pathFrom);
            FileSystemInfo toFI = fis.FirstOrDefault(fi => fi.Name == _pathTo);

            if (fromFI is DirectoryInfo && toFI is FileInfo) 
                throw new CommandException("Copy directory to file");

            if (fromFI is DirectoryInfo fromDirectory)
            {
                RecursiveCopy(fromDirectory, (DirectoryInfo)toFI);
            }
            else if (fromFI is FileInfo fromFile)
            {
                CopyFileTo(fromFile, toFI);
            }
        }

        private void CopyFileTo(FileInfo fromFile, FileSystemInfo toFI)
        {
            if (toFI is null)
            {
                fromFile.CopyTo(Path.Combine(_fileManagerModel.CurrentDirectory.FullName, _pathTo));
            }
            else if (toFI is DirectoryInfo toDirectory)
            {
                try
                {
                    fromFile.CopyTo(Path.Combine(toDirectory.FullName, _pathTo), _forced);
                }
                catch (IOException)
                {
                    List<FileConflict> fileConflicts = new List<FileConflict>();
                    fileConflicts.Add(new FileConflict(fromFile, toFI));

                    throw new FileConflictException(fileConflicts);
                }
            }  
            else if (toFI is FileInfo)
            {
                if (_forced)
                {
                    fromFile.CopyTo(toFI.FullName, true);
                }
                else
                {
                    List<FileConflict> fileConflicts = new List<FileConflict>();
                    fileConflicts.Add(new FileConflict(fromFile, toFI));

                    throw new FileConflictException(fileConflicts);
                }
            }
        }

        private void RecursiveCopy(DirectoryInfo fromDirectory, DirectoryInfo toDirectory)
        {
            if (toDirectory == null)
                toDirectory = new DirectoryInfo(Path.Combine(_fileManagerModel.CurrentDirectory.FullName, _pathTo));

            if (!toDirectory.Exists)
                toDirectory.Create();

            foreach (FileSystemInfo entry in fromDirectory.GetFileSystemInfos())
            {
                if (entry is FileInfo fileEntry)
                {
                    fileEntry.CopyTo(Path.Combine(toDirectory.FullName, fileEntry.Name), _forced);
                }
                else if (entry is DirectoryInfo directoryEntry)
                {
                    RecursiveCopy(directoryEntry, new DirectoryInfo(Path.Combine(toDirectory.FullName, directoryEntry.Name)));
                }
            }
        }
    }

    class CopyCommandOptions : CommandlineOptions
    {
        [Option("-t", "--to", HasValue = true, HelpText = "Destination file or directory", Required = true)]
        public string To { get; set; }

        [Option("-f", "--from", HasValue = true, HelpText = "Origin file or directory", Required = true)]
        public string From { get; set; }

        [Option("-o", "--overwrite", HelpText = "Ignore conflicts")]
        public bool Overwrite { get; set; }

        [OptionsDescription]
        public string CommandDescription { get; set; } = "Copy files or directories, specify -o to ignore conflicts";
    }
}
