using System;

namespace AlgLesson4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<int> tree = new(10)
            {
                1, 4, 6, 2, -5, 11, 29, -5, 7, 6, 12
            };

            tree.DebugPrint();
            Console.WriteLine(new string('_', 100));

            Console.WriteLine("Remove 4:");
            tree.Remove(4);
            tree.DebugPrint();
            Console.WriteLine(new string('_', 100));

            Console.WriteLine(new string('_', 100));
            Console.WriteLine("Remove -5:");
            tree.Remove(-5);
            tree.DebugPrint();

            Console.WriteLine(new string('_', 100));
            Console.WriteLine("Add 4:");
            tree.Add(4);
            tree.DebugPrint();

            Console.WriteLine("Remove 2 recursively:");
            tree.Remove(2, true);
            tree.DebugPrint();
            Console.WriteLine(new string('_', 100));
        }
    }
}
