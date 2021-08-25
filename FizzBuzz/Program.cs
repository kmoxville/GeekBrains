using System;
using System.Linq;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            var fizzBuzzQuery = Enumerable.Range(1, 100).Select(
                element => {
                    string result;

                    if (element % 3 == 0 && element % 5 == 0) 
                    {
                        result = "fizz buzz";
                    }
                    else if (element % 3 == 0)
                    {
                        result = "fizz";
                    }
                    else if (element % 5 == 0)
                    {
                        result = "buzz";
                    }
                    else
                    {
                        result = element.ToString();
                    }

                    return result;
                });

            foreach (string str in fizzBuzzQuery)
            {
                Console.WriteLine(str);
            }
        }
    }
}
