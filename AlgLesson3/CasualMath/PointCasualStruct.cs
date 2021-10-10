using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson3
{
    public struct PointCasualStructDouble
    {
        public PointCasualStructDouble(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; init; }
        public double Y { get; init; }
    }

    public struct PointCasualStructFloat
    {
        public PointCasualStructFloat(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; init; }
        public float Y { get; init; }
    }
}
