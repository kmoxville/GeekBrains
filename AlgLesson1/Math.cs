using System;
using System.Collections.Generic;
using System.Text;

namespace AlgLesson1
{
    static class Math
    {
        /// <summary>
        /// Задание 1
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsNumberPrime(int number)
        {
            int d = 0, i = 2;

            while (i < number)
            {
                if (number % i == 0)
                    d++;
                
                i++;
            }

            if (d == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Задание 3. Ряд Фибоначчи с рекурсией
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int FiboRecursive(int number)
        {
            if (number <= 1)
                return number;

            return FiboRecursive(number - 1) + FiboRecursive(number - 2);
        }

        /// <summary>
        /// Задание 3. ряд Фибоначчи без рекурсии
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int FiboCycle(int number)
        {
            if (number <= 1)
                return number;

            int result = 0, prevResult = 1, tmp = 0;

            for (int i = 0; i < number; i++)
            {
                tmp = result;
                result = prevResult;
                prevResult += tmp;
            }

            return result;
        }
    }
}
