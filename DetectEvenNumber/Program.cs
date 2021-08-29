using System;

namespace DetectEvenNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число");

            int number          = int.Parse(Console.ReadLine());
            string numberType   = (number % 2 == 0 ? "четное" : "нечетное");

            Console.WriteLine($"Число {number} {numberType}") ;
        }
    }
}
