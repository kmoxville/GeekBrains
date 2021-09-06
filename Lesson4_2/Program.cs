using System;
using System.Linq;

namespace Lesson4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку чисел");
            
            string userInput = Console.ReadLine();
            var userInputParsedQuery = userInput.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(element => 
                { 
                    bool parseSuccesfull = decimal.TryParse(element, out var parseResult); 
                    return (parseSuccesfull, parseResult, element); 
                });

            decimal sum = userInputParsedQuery.Where(element => element.Item1)
                .Select(element => element.Item2)
                .Sum();

            var unparsedInputQuery = userInputParsedQuery.Where(element => !element.Item1)
                .Select(element => element.Item3);

            Console.WriteLine("Нераспозанный ввод: {0}", string.Join(' ', unparsedInputQuery));
            Console.WriteLine($"Сумма: {sum}");
        }
    }
}
