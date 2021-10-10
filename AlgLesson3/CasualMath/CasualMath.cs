using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson3
{
    public static class CasualMath
    {
        public static double CountDistance(PointCasualDouble firstPoint, PointCasualDouble secondPoint)
        {
            double result;
            double deltaX, deltaY;

            deltaX = firstPoint.X - secondPoint.X;
            deltaY = firstPoint.Y - secondPoint.Y;

            result = System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return result;
        }

        public static double CountDistance(PointCasualFloat firstPoint, PointCasualFloat secondPoint)
        {
            double result;
            double deltaX, deltaY;

            deltaX = firstPoint.X - secondPoint.X;
            deltaY = firstPoint.Y - secondPoint.Y;

            result = System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return result;
        }
    }
}
