using System;

namespace GetFullName
{
    class Program
    {
        static string GetFullName(string firstName, string lastName, string patronymic = "")
        {
            return string.Join(' ', lastName, firstName, patronymic);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(GetFullName("Иванов", "Иван", "Иванович"));
            Console.WriteLine(GetFullName("Петров", "Петр", "Петрович"));
            Console.WriteLine(GetFullName("Васильев", "Василий", "Васильевич"));
        }
    }
}
