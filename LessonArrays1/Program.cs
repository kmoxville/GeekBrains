using System;
using System.Text;

namespace LessonArrays1
{
    class Program
    {
        private const int ARRAY_LENGTH = 11;

        static void Main(string[] args)
        {
            var rand = new Random();
            int[][] array2d = new int[ARRAY_LENGTH][];

            for (int i = 0; i < array2d.Length; i++)
            {
                array2d[i] = new int[ARRAY_LENGTH];
                for (int j = 0; j < array2d[i].Length; j++)
                {
                    array2d[i][j] = rand.Next(0, 1000);
                }
            }

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < array2d.Length; i++)
            {
                for (int j = 0; j < array2d[i].Length; j++)
                {
                    if ((i == j) || (i+j == array2d.Length-1))
                        result.Append($"{array2d[i][j], 8}");
                    else
                        result.Append("".PadLeft(8));
                }

                result.AppendLine();
            }

            Console.WriteLine(result);
        }
    }
}
