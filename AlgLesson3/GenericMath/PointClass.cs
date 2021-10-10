using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson3
{
    public sealed class PointClass<T> : IPoint<T>
    {
        public PointClass(T x, T y)
        {
            X = x;
            Y = y;
        }

        public T X { get; init; }
        public T Y { get; init; }
    }
}
