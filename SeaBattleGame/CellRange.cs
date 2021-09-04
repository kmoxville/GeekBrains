using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattleGame
{
    enum CellRangeOrientaion
    {
        Horizontal,
        Vertical
    }

    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class CellRange
    {
        public CellRange(CellRangeOrientaion orientation)
        {
            Orientation = orientation;
        }

        public CellRangeOrientaion Orientation { get; }
        public Point FirstPoint { get; set; }
        public Point SecondPoint { get; set; }

        public int Length
        {
            get
            {
                return (Orientation == CellRangeOrientaion.Horizontal ? SecondPoint.Y - FirstPoint.Y : SecondPoint.X - FirstPoint.X);
            }
        }
    }
}
