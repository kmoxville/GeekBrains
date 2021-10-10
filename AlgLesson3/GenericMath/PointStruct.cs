using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson3
{
    public struct PointStruct<T> : IPoint<T>
    {
        public PointStruct(T x, T y)
        {
            X = x;
            Y = y;
        }

        public T X { get; init; }
        public T Y { get; init; }
    }
}
