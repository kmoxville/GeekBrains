using System;

namespace AlgLesson1
{
    class Program
    {
        static int[] primeNumbersTestArray = { 1, 17, 47, 117, 163, 164 };
        static int[] fiboNumbersTestArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20 };

        static void Main(string[] args)
        {
            Console.WriteLine("Prime test");
            foreach (int i in primeNumbersTestArray)
            {
                string description = Math.IsNumberPrime(i) ? "prime" : "not prime";
                Console.WriteLine($"{i} is {description}");
            }

            Console.WriteLine("\nFibonacci recursive test");
            foreach (int i in fiboNumbersTestArray)
            {
                Console.WriteLine($"F({i}) = {Math.FiboRecursive(i)}");
            }

            Console.WriteLine("\nFibonacci cycle test");
            foreach (int i in fiboNumbersTestArray)
            {
                Console.WriteLine($"F({i}) = {Math.FiboCycle(i)}");
            }
        }
    }
}
