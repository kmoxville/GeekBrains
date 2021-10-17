using System;

namespace AlgLesson5
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<int> tree = new()
            {
                100,
                50,
                80,
                60
            };

            Console.WriteLine("initial tree");
            tree.DebugPrint();

            Console.WriteLine("Add 85");
            tree.Add(85);
            tree.DebugPrint();

            Console.WriteLine("Add 84");
            tree.Add(84);
            tree.DebugPrint();

            Console.WriteLine("Add 86");
            tree.Add(86);
            tree.DebugPrint();

            Console.WriteLine("Add 120");
            tree.Add(120);
            tree.DebugPrint();

            Console.WriteLine("DFS, value = 200: (in debug output window)");
            tree.IndexOf(200, SearchKind.DFS);

            Console.WriteLine("BFS, value = 200: (in debug output window)");
            tree.IndexOf(200, SearchKind.BFS);
        }
    }
}
