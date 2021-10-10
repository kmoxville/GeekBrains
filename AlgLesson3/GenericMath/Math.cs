using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson3
{
    public static class Math
    {
        /// <summary>
        /// CountDistance<T> уточнена для PointClass<double> и PointStruct<float>
        /// Если нет уточненной версии, вызывается эта версия, подразумевающая виртуальные вызывы и невозможность инлайнинга функций
        /// Поэтому результат в бенчмарке существенно отличается
        /// </summary>
        public static double CountDistance<T>(IPoint<T> firstPoint, IPoint<T> secondPoint, IMathProvider<T> mathProvider)
        {
            double result;
            T deltaX, deltaY;

            deltaX = mathProvider.Subtract(firstPoint.X, secondPoint.X);
            deltaY = mathProvider.Subtract(firstPoint.Y, secondPoint.Y);

            result = System.Math.Sqrt(
                (double)Convert.ChangeType(
                    mathProvider.Add(
                        mathProvider.Multiply(deltaX, deltaX),
                        mathProvider.Multiply(deltaY, deltaY)), 
                typeof(double)));

            return result;
        }

        public static double CountDistance(PointClass<double> firstPoint, PointClass<double> secondPoint)
        {
            double result;
            double deltaX, deltaY;
            DoubleMathProvider mathProvider = new();

            deltaX = mathProvider.Subtract(firstPoint.X, secondPoint.X);
            deltaY = mathProvider.Subtract(firstPoint.Y, secondPoint.Y);

            result = System.Math.Sqrt(
                mathProvider.Add(
                    mathProvider.Multiply(deltaX, deltaX),
                    mathProvider.Multiply(deltaY, deltaY)));

            return result;
        }

        public static double CountDistance(PointStruct<float> firstPoint, PointStruct<float> secondPoint)
        {
            double result;
            float deltaX, deltaY;
            FloatMathProvider mathProvider = new();

            deltaX = mathProvider.Subtract(firstPoint.X, secondPoint.X);
            deltaY = mathProvider.Subtract(firstPoint.Y, secondPoint.Y);

            result = System.Math.Sqrt(
                mathProvider.Add(
                    mathProvider.Multiply(deltaX, deltaX),
                    mathProvider.Multiply(deltaY, deltaY)));

            return result;
        }
    }
}
