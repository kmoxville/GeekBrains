using System;
using System.Linq;
using System.IO;

namespace Lesson5_3
{
    class Program
    {
        const string FILE_NAME = "binary_file.dat";

        static void Main(string[] args)
        {
            Console.WriteLine("Input integer(0..255) sequence");
            string userInput = Console.ReadLine();

            var inputQuery = userInput
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(element =>
                {
                    bool parsingIsSuccessful = byte.TryParse(element, out byte result);
                    return (ParsingIsSuccessful: parsingIsSuccessful, Result: result, ResultUnparsed: element);
                });

            var unparsedValuesQuery = inputQuery
                .Where(element => !element.ParsingIsSuccessful)
                .Select(element => element.ResultUnparsed).ToArray();

            string unparsedValuesString = string.Join(',', unparsedValuesQuery);
            if (unparsedValuesString.Length > 0)
                Console.WriteLine($"Unparsed values: {unparsedValuesString}");

            var parsedValues = inputQuery
                .Where(element => element.ParsingIsSuccessful)
                .Select(element => element.Result);

            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    foreach (byte value in parsedValues)
                    {
                        bw.Write(value);
                    }
                }
            }     
        }
    }
}
