using System;
using System.Text;
using static SeaBattleGame.SeaBattle;

namespace SeaBattleGame
{
    class Program
    {
        private const char square = '\u25A2';
        private const char point = '\u2022';

        static void Main(string[] args)
        {
            SeaBattle seaBattle = new SeaBattle();
            
            seaBattle.AddShip(new Ship(1));
            seaBattle.AddShip(new Ship(1));
            seaBattle.AddShip(new Ship(1));
            seaBattle.AddShip(new Ship(1));

            seaBattle.AddShip(new Ship(2));
            seaBattle.AddShip(new Ship(2));
            seaBattle.AddShip(new Ship(2));

            seaBattle.AddShip(new Ship(3));
            seaBattle.AddShip(new Ship(3));

            //seaBattle.AddShip(new Ship(4));

            seaBattle.GenerateMap();

            var map = seaBattle.Map;

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                if (i == 0)
                    result.AppendLine(new string('_', map.GetLength(0) + 1));

                for (int j = 0; j < map.GetLength(0); j++)
                {
                    if (j == 0)
                        result.Append("|");

                    switch (map[i, j])
                    {
                        case CellState.Ship:
                            result.Append("X");
                            break;
                        case CellState.ShipNearby:
                            result.Append("0"); //result.Append("+");
                            break;
                        default:
                            result.Append("0");
                            break;
                    }
                }

                result.AppendLine();
            }

            Console.Clear();
            Console.WriteLine(result.ToString());
        }
    }
}
