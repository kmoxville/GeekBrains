using System;

namespace AverageTemperature
{
    class Program
    {
        private const double MINIMAL_CELSIUS_TEMP = -273.15;

        static void Main(string[] args)
        {
            try
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

                Console.WriteLine($"Average temperature is {Math.Round((maxTemp + minTemp) / 2D, 1)}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid user input");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Too hot...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
