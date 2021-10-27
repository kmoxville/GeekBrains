using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgLesson8
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] testArray = new int[100];
            Random rand = new Random();

            for (int i = 0; i < testArray.Length; i++)
            {
                testArray[i] = rand.Next(0, 100);
            }

            Console.WriteLine("Unsorted array:");
            Console.WriteLine(string.Join(", ", testArray));

            var result = BucketSort(testArray, 4);

            Console.WriteLine("Sorted array:");
            Console.WriteLine(string.Join(", ", result));
        }

        public static List<int> BucketSort(ICollection<int> collection, int n)
        {
            List<int>[] buckets = new List<int>[n];
            List<int> result = new List<int>(collection.Count);

            int min = Enumerable.Min(collection);
            int max = Enumerable.Max(collection);
            int range = (max - min) / n + 1;

            for (int i = 0; i < n; i++)
            {
                buckets[i] = new List<int>();
            }

            foreach (var item in collection)
            {
                buckets[(item - min) / range].Add(item);
            }

            foreach (var bucket in buckets)
            {
                result.AddRange(bucket.OrderBy(a => a));
            }

            return result;
        }
    }
}
