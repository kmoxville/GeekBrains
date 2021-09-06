using System;
using System.Collections.Generic;
using System.Linq;

namespace SeaBattleGame
{
    class SeaBattle
    {
        [Flags]
        public enum CellState
        {
            Empty       = 0b0000_0000,
            Ship        = 0b0000_0001,
            Miss        = 0b0000_0010,
            Hit         = 0b0000_0100,
            ShipNearby  = 0b0000_1000
        }

        private CellState[,] map;
        private List<Ship> ships = new List<Ship>();

        public SeaBattle(uint size = 10)
        {
            map = new CellState[size, size];
        }

        public CellState[,] Map => (CellState[,])map.Clone();

        public void AddShip(Ship ship) => ships.Add(ship);

        public void GenerateMap()
        {
            if (ships.Count == 0)
                throw new Exception("Не заданы корабли");

            foreach (var ship in ships)
            {
                var freeSpaces = GetFreeSpaces(CellRangeOrientaion.Horizontal).Concat(GetFreeSpaces(CellRangeOrientaion.Vertical));
                PlaceShip(ship, freeSpaces);
            }
        }

        /// <summary>
        /// Размещает корабль на карте в доступном месте
        /// </summary>
        /// <param name="ship">Корабль</param>
        /// <param name="freeSpaces">Доступные диапазоны размещения</param>
        private void PlaceShip(Ship ship, IEnumerable<CellRange> freeSpaces)
        {
            //1. выбирается рандомное место для корабля(в котором можно разместить корабль требуемого размера)
            var rand = new Random();
            freeSpaces = freeSpaces.Where(element => element.Length >= ship.Size).ToList();

            if (freeSpaces.Count() == 0)
                throw new Exception("No available space for ship");

            CellRange cellRangePick     = freeSpaces.ElementAt(rand.Next(0, freeSpaces.Count()));
            var orientation             = cellRangePick.Orientation;
            int from                    = (orientation == CellRangeOrientaion.Vertical ? cellRangePick.FirstPoint.X : cellRangePick.FirstPoint.Y);
            int to                      = (orientation == CellRangeOrientaion.Vertical ? cellRangePick.SecondPoint.X : cellRangePick.SecondPoint.Y);
            int pointer                 = (orientation == CellRangeOrientaion.Vertical ? cellRangePick.FirstPoint.Y : cellRangePick.FirstPoint.X);

            //2. внутри подходящего места выбирается рандомная точка старта корабля и он растет влево(вниз) и вправо(вверх) поочередно по одной клетке
            int shipStart               = rand.Next(from, to);
            int shipEnd                 = shipStart;
            int currentShipLength       = 1;

            bool goBack = true;
            while (currentShipLength != ship.Size)
            {
                if (goBack)
                {
                    if (canGoBack(from, shipStart))
                    {
                        shipStart--;
                        currentShipLength++;
                    }
                }
                else
                {
                    if (canGoForward(to, shipEnd))
                    {
                        shipEnd++;
                        currentShipLength++;
                    }
                }

                goBack = !goBack;
            }

            //3. помечаем клетки ShipNearby, Ship
            for (int i = shipStart; i <= shipEnd; i++)
            {
                int x = (orientation == CellRangeOrientaion.Horizontal ? pointer : i);
                int y = (orientation == CellRangeOrientaion.Horizontal ? i : pointer);

                MarkNearby(x, y);
                map[x, y] = CellState.Ship;
            }
        }

        /// <summary>
        /// Отмечает флагом Nearby клетки, смежные с кораблем
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void MarkNearby(int x, int y)
        {
            x = x == 0 ? 0 : x - 1;
            y = y == 0 ? 0 : y - 1;

            int xBoundary = x + 3;
            int yBoundary = y + 3;

            xBoundary = xBoundary > map.GetLength(0) ? map.GetLength(0) : xBoundary;
            yBoundary = yBoundary > map.GetLength(0) ? map.GetLength(0) : yBoundary;

            for (int i = x; i < xBoundary; i++)
            {
                for (int j = y; j < yBoundary; j++)
                {
                    if (map[i, j] == CellState.Empty)
                        map[i, j] = CellState.ShipNearby;
                }
            }
        }

        /// <summary>
        /// Возвращает коллекцию доступных незанятых пространст 
        /// </summary>
        /// <param name="orientation">требуюмая ориентация пространства</param>
        /// <returns></returns>
        private List<CellRange> GetFreeSpaces(CellRangeOrientaion orientation)
        {
            var freeSpaces = new List<CellRange>();

            CellRange currentRange = null;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    int x = (orientation == CellRangeOrientaion.Horizontal ? i : j);
                    int y = (orientation == CellRangeOrientaion.Horizontal ? j : i);
                    if (map[x,y] == CellState.Empty)
                    {
                        if (currentRange == null)
                        {
                            currentRange = new CellRange(orientation) { FirstPoint = new Point() { X = x, Y = y }, SecondPoint = new Point() { X = x, Y = y } };
                        }
                        else
                        {
                            var point = currentRange.SecondPoint;
                            if (orientation == CellRangeOrientaion.Horizontal)
                                point.Y = y;
                            else
                                point.X = x;

                            currentRange.SecondPoint = point;
                        }
                    }
                    else
                    {
                        if (currentRange != null)
                        {
                            freeSpaces.Add(currentRange);
                            currentRange = null;
                        }
                    }
                }

                if (currentRange != null)
                {
                    freeSpaces.Add(currentRange);
                    currentRange = null;
                }
            }

            return freeSpaces;
        }

        private bool canGoForward(int to, int shipEnd)
        {
            return to > shipEnd;
        }

        private bool canGoBack(int from, int shipStart)
        {
            return from < shipStart;
        }
    }
}
