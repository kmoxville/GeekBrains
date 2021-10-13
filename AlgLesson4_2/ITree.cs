using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson4_2
{
    public interface ITree<T>
    {
        void Add(T value);
        void Remove(T value);
        ITree<T> this[T value] { get; }
    }
}
