using System;

namespace Lesson4_3
{
    class Program
    {
        enum Season
        {
            None,
            Winter,
            Spring,
            Summer,
            Autumn
        }

        static Season GetSeason(int month)
        {
            return month switch
            {
                int i when (i == 1 || i == 1 || i == 12) => Season.Winter,
                int i when (i >= 3 && i <= 5) => Season.Spring,
                int i when (i >= 6 && i <= 8) => Season.Summer,
                int i when (i >= 9 && i <= 11) => Season.Autumn,
                _ => Season.None
            };
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите номер месяца");

            bool parsingIsSuccesfull = int.TryParse(Console.ReadLine(), out int monthNumber);
            if (parsingIsSuccesfull && monthNumber >= 1 && monthNumber <= 12)
            {
                Console.WriteLine(GetSeason(monthNumber));
            }
            else
            {
                Console.WriteLine("Ошибка: введите число от 1 до 12");
            }
        }
    }
}
