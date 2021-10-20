using System;

namespace AlgLesson6
{
    class Program
    {
        /// <summary>
        /// Представление графа: https://prnt.sc/1wuo546
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Graph<int> graph = new Graph<int>()
            {
                (1, new int[] {2, 5, 7, 8}),
                (2, new int[] {1, 6}),
                (3, new int[] {2, 6, 4}),
                (4, new int[] {3, 5}),
                (5, new int[] {1, 6, 4}),
                (6, new int[] {5, 2, 3}),
                (7, new int[] {1}),
                (8, new int[] {1})
            };

            Console.WriteLine("Graph:");
            Console.WriteLine("DFS, start vertex = 1:");
            var result = graph.DFS(1);
            Console.WriteLine(string.Join("->", result));

            Console.WriteLine("BFS, start vertex = 1:");
            result = graph.BFS(1);
            Console.WriteLine(string.Join("->", result));
        }
    }
}
