using System;

namespace Lesson3_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            string[,] catalog = new string[5, 2];
            int currentIndex = 0;

            while (true)
            {
                Console.WriteLine("1) Добавить новый эелемент\n2) Вывести список\n3) Выйти");

                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    if (currentIndex == catalog.GetLength(0))
                    {
                        Console.WriteLine("Список переполнен");
                        continue;
                    }

                    Console.WriteLine("Введите имя");
                    catalog[currentIndex, 0] = Console.ReadLine();
                    Console.WriteLine("Введите номер телефона");
                    catalog[currentIndex, 1] = Console.ReadLine();

                    currentIndex++;
                }
                else if (answer == "2")
                {
                    Console.WriteLine("Имя".PadLeft(30) + "|" + "Телефон".PadLeft(30));
                    for (int i = 0; i < currentIndex; i++)
                    {
                        Console.WriteLine(catalog[i, 0].PadLeft(30) + "|" + catalog[i, 1].PadLeft(30));
                    }
                }
                else if (answer == "3")
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
