using System;
using System.Collections.Generic;

namespace AlgLesson2
{
    class Program
    {
        /// <summary>
        /// Добавляет 10 элементов в лист, делает по три вставки/удаления в случайные индексы
        /// Затем бинарный поиск
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            LinkedList<int> list = new();

            TestAdd(list, 10);
            ShowArray(list);

            TestInsert(list, 3);
            ShowArray(list);

            TestRemove(list, 3);
            ShowArray(list);

            TestBinarySearch();
        }

        private static void TestBinarySearch()
        {
            Random rand = new Random();
            List<int> list = new(20);
            for (int i = 0; i < 20; i++)
            {
                list.Add(rand.Next(0, 50));
            }

            list.Sort();
            ShowArray(list);

            int searchValue = rand.Next(0, 20);
            int result = Utils.BinarySearch(list, searchValue);
            Console.WriteLine($"Searching for {searchValue}");
            if (result != -1)
                Console.WriteLine($"Element found at [{result}]");
            else
                Console.WriteLine("Element not found");
        }

        private static void TestRemove(LinkedList<int> list, int count)
        {
            Random rand = new Random();

            for (int i = 0; i < count; i++)
            {
                int index = rand.Next(0, list.Count);
                list.RemoveAt(index);
                Console.WriteLine($"Removed at {index}");
                ShowArray(list);
            }
        }

        private static void TestInsert(LinkedList<int> list, int count)
        {
            Random rand = new Random();

            for (int i = 0; i < count; i++)
            {
                int value = rand.Next(1, 100);
                int index = rand.Next(0, list.Count);
                list.Insert(index, value);
                Console.WriteLine($"Inserted {value} at {index}");
                ShowArray(list);
            }
        }

        private static void ShowArray(IEnumerable<int> list)
        {
            Console.WriteLine("Current list state:");
            int i = 0;
            foreach (int value in list)
            {
                Console.WriteLine($"[{i}]\t=\t{value}");
                i++;
            }
        }

        private static void TestAdd(LinkedList<int> list, int size)
        {
            Random rand = new Random();

            for (int i = 0; i < size; i++)
            {
                list.Add(rand.Next(1, 100));
            }
        }
    }
}
