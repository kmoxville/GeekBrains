using System;

namespace MessingAroundWithMonth
{
    class Program
    {
        private const double MINIMAL_CELSIUS_TEMP = -273.15;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите номер месяца");

                int monthNumber = int.Parse(Console.ReadLine());
                if (monthNumber < 1 || monthNumber > 12)
                {
                    throw new Exception("Номер месяца должен быть в пределах от 1 до 12");
                }

                double averageTemp = GetAverageTemperature();

                Console.WriteLine($"Название месяца: {new DateTime(1, monthNumber, 1):MMMM}");

                if (averageTemp > 0 
                    && (monthNumber == 12 || monthNumber == 1 || monthNumber == 2))
                {
                    Console.WriteLine($"Дождливая зима");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static double GetAverageTemperature()
        {
            string userInput;
            int minTemp, maxTemp;

            Console.WriteLine("Input minimal temperature (C)");
            userInput = Console.ReadLine();
            minTemp = int.Parse(userInput);

            if (minTemp < MINIMAL_CELSIUS_TEMP)
            {
                throw new Exception($"Temp is below {MINIMAL_CELSIUS_TEMP}");
            }

            Console.WriteLine("Input maximum temperature (C)");
            userInput = Console.ReadLine();
            maxTemp = int.Parse(userInput);

            return Math.Round((maxTemp + minTemp) / 2D, 1);
        }
    }
}
