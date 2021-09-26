using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    public interface ICommand
    {
        /// <summary>
        /// Базовый интерфейс команды
        /// </summary>
        /// <param name="fileManagerModel"></param>
        /// <param name="options">Опции(например, из командной строки)</param>
        void Execute(IFileManagerModel fileManagerModel, CommandlineOptions options);
    }
}
