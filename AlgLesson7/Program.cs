using System;

namespace AlgLesson7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите N");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите M");
            int m = int.Parse(Console.ReadLine());

            int[,] field = new int[n - 1, m - 1];

            Console.WriteLine($"Количество маршрутов = {W(n - 1, m - 1)}");
        }

        private static int W(int n, int m)
        {
            if ((n == 0) || (m == 0))
                return 1;

            return W(n - 1, m) + W(n, m - 1);
        }
    }
}
