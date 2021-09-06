using System;
using System.Linq;

namespace Lesson3_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string hello = "Hello";

            for (int i = 1; i <= hello.Length; i++)
            {
                Console.Write(hello[^i]);
            }

            Console.WriteLine();

            Console.WriteLine(Enumerable.Reverse(hello).ToArray());
        }
    }
}
