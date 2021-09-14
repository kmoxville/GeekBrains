using System;
using System.IO;

namespace Lesson5_2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamWriter sw = new StreamWriter("startup.txt", true))
            {
                sw.WriteLine(DateTime.Now);
            }
        }
    }
}
