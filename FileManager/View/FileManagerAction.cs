using CmdParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Соединяет ICommand и опции командной строки
    /// Ключевой метод Execute - парсит аргументы команды, выполняет команду
    /// </summary>
    class FileManagerAction
    {
        public FileManagerAction(string commandName, ICommand command, CommandlineOptions actionOptions)
        {
            CommandName = commandName;
            Command = command;
            ActionOptions = actionOptions;
        }

        public string CommandName { get; private set; }
        public ICommand Command { get; private set; }
        public CommandlineOptions ActionOptions { get; private set; }

        public virtual string Execute(FileManagerModel fileManagerModel, string[] options)
        {
            Parser parser = new Parser(options);
            parser.Parse(ActionOptions);

            if (parser.HadErrors)
            {
                return parser.Usage;
            }

            fileManagerModel.ExecuteCommand(Command, ActionOptions);

            return string.Empty;
        }
    }
}
