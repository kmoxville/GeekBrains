using CmdParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManager
{
    public sealed class ChangeDirCommand : ICommand
    {
        private string _target;
        private IFileManagerModel _fileManagerModel;

        public void Execute(IFileManagerModel fileManagerModel, CommandlineOptions cmdOptions)
        {
            var options = cmdOptions as ChangeDirCommandOptions;

            _target = options.Target;
            _fileManagerModel = fileManagerModel;

            _fileManagerModel.ChangeDirectory(_target);
        }
    }

    class ChangeDirCommandOptions : CommandlineOptions
    {
        [Option("-t", "--target", HasValue = true, Required = true, HelpText = "Target file or folder")]
        public string Target { get; set; }

        [OptionsDescription]
        public string CommandDescription { get; set; } = "Change current directory";
    }
}
