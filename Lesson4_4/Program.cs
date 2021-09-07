using System;
using System.Linq;

namespace Lesson4_4
{
    class Program
    {
        static int RomeCharToInt(char ch) => ch switch
        {
            'I' => 1,
            'V' => 5,
            'X' => 10,
            'L' => 50,
            'C' => 100,
            'D' => 500,
            'M' => 1000,
            _ => throw new ArgumentException("Неизвестный символ")
        }; 

        static int RomeNumberToInt(string romeNumber)
        {
            int result          = 0;
            int prevValue       = 0;
            int currentValue    = 0;

            romeNumber = romeNumber.ToUpper();

            foreach (char ch in romeNumber.ToCharArray().Reverse())
            {
                currentValue = RomeCharToInt(ch);
                result += (currentValue >= prevValue ? currentValue : -currentValue);
                prevValue = currentValue;
            }

            return result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите число в римской системе счисления");
            Console.WriteLine(RomeNumberToInt(Console.ReadLine()));
        }
    }
}
