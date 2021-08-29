using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName;

            Console.WriteLine("Введите имя");
            userName = Console.ReadLine();
            Console.WriteLine($"Привет, {userName}, сегодня {DateTime.Today:D}");
        }
    }
}
