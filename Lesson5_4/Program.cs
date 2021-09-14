using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Lesson5_4
{
    class Program
    {
        /// <summary>
        /// Выводит содержимое каталога в виде дерева
        /// </summary>
        /// <param name="directoryInfo">Каталог</param>
        /// <param name="recursively">Рекурсивно</param>
        /// <returns>Содержимое каталога в виде дерева</returns>
        public static string GetDirectoryTree(DirectoryInfo directoryInfo, bool recursively = true)
        {
            return GetDirectoryTree(new StringBuilder(), directoryInfo, recursively, 0).ToString();
        }

        /// <summary>
        /// Для внутреннего использования, использует один общий StringBuilder
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="recursively"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private static StringBuilder GetDirectoryTree(StringBuilder stringBuilder, DirectoryInfo directoryInfo, bool recursively = true, int step = 0)
        {
            foreach (var fsInfo in directoryInfo.GetFileSystemInfos())
            {
                stringBuilder.Append(new string('\t', step));
                stringBuilder.AppendLine(fsInfo.Name);

                if (recursively && fsInfo is DirectoryInfo dirInfo)
                {
                    GetDirectoryTree(stringBuilder, dirInfo, true, step + 1);
                }
            }

            return stringBuilder;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Input path to directory");
            string userInput = Console.ReadLine();

            DirectoryInfo di = new DirectoryInfo(userInput);
            if (!di.Exists)
            {
                Console.WriteLine("Directory does not exit");
                Environment.Exit(-1);
            }

            Console.WriteLine("Input file path to save directory tree");
            userInput = Console.ReadLine();

            Console.WriteLine("Recursively? (y/n)");
            bool recursevely = Console.ReadLine() == "y";

            using (StreamWriter sw = new StreamWriter(userInput, false))
            {
                sw.Write(GetDirectoryTree(di, recursevely));
            }
        }
    }
}
