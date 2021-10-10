using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson3
{
    public interface IMathProvider<T>
    {
        public T Add(T a, T b);

        public T Subtract(T a, T b) => Add(a, Negate(b));

        public T Multiply(T a, T b);

        public T Divide(T a, T b);

        public T Negate(T a);
    }
}
