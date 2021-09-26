using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    /// <summary>
    /// Модель реализует базовый функционал для выполнения файловых команд
    /// </summary>
    public interface IFileManagerModel
    {
        /// <summary>
        /// Текущая директория
        /// </summary>
        DirectoryInfo CurrentDirectory { get; set; }

        /// <summary>
        /// Сменить директорию
        /// </summary>
        /// <param name="path">Путь к новой директории</param>
        void ChangeDirectory(string path);

        /// <summary>
        /// Содержимое директории
        /// </summary>
        FileSystemInfo[] Content { get; }

        /// <summary>
        /// Выполняет команду
        /// </summary>
        /// <param name="command">Команда(копирование, удаление, смена директории)</param>
        /// <param name="options">Опции команды(получаются из командной строки)</param>
        void ExecuteCommand(ICommand command, CommandlineOptions options);
    }
}
