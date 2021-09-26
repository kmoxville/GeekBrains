using CmdParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManager
{
    public sealed class RemoveCommand : ICommand
    {
        private string _path;
        private bool _recursive;
        private IFileManagerModel _fileManagerModel;

        public void Execute(IFileManagerModel fileManagerModel, CommandlineOptions cmdOptions)
        {
            var options = cmdOptions as RemoveCommandOptions;

            _path = options.Target;
            _recursive = options.Overwrite;
            _fileManagerModel = fileManagerModel;
            var fis = fileManagerModel.Content;

            FileSystemInfo fromFI = fis.First(fi => fi.Name == _path);

            if (fromFI is DirectoryInfo fromDirectory)
            {
                try
                {
                    fromDirectory.Delete(_recursive);
                }
                catch (IOException)
                {
                    throw new DirectoryNotEmptyException() { Path = fromFI.FullName };
                }
            }
            else if (fromFI is FileInfo fromFile)
            {
                fromFI.Delete();
            }
        }
    }

    class RemoveCommandOptions : CommandlineOptions
    {
        [Option("-r", "--recursive", HelpText = "Recursively")]
        public bool Overwrite { get; set; }

        [Option("-t", "--target", HasValue = true, Required = true, HelpText = "Target file or folder")]
        public string Target { get; set; }

        [OptionsDescription]
        public string CommandDescription { get; set; } = "Remove files or directories, if directory not empty specify -r parameter";
    }
}
