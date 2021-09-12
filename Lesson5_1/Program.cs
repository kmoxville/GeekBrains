using System;
using System.Linq;
using System.IO;

namespace Lesson5_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input name of file");

            string fileName = Console.ReadLine();
            if (fileName.Length == 0)
            {
                Console.WriteLine("Filename cant be empty");
                Environment.Exit(-1);
            }

            char[] invalidChars = Path.GetInvalidFileNameChars();

            var invalidCharsList = fileName
                .Select((ch, index) => (Char: ch, Index: index))
                .Where(element => invalidChars.Contains(element.Char)).ToList();

            if (invalidCharsList.Count > 0)
            {
                Console.WriteLine("Invalid characters:");
                invalidCharsList.ForEach(element => Console.WriteLine($"Symbol \"{element.Char}\", position {element.Index + 1}"));
                Environment.Exit(-1);
            }

            Console.WriteLine("Input content of file");
            string fileContent = Console.ReadLine();

            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                sw.WriteLine(fileContent);
                Console.WriteLine($"File overwritten: {Path.Combine(Environment.CurrentDirectory, fileName)}");
            }
        }
    }
}
