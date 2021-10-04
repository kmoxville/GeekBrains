using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson2
{
    static class Utils
    {
        public static int BinarySearch<T>(IReadOnlyList<T> collection, T value, IComparer<T> comparer = null)
        {
            int size = collection.Count;

            if (comparer == null)
                comparer = Comparer<T>.Default;

            //нижняя, верхняя границы и медиана
            int low = 0, high = size, middle;

            while (low != high)
            {
                middle = (high + low) >> 1;

                int comparisonResult = comparer.Compare(collection[middle], value);

                if (comparisonResult == 0)
                    return middle;

                low = comparisonResult < 0 ? middle + 1 : low;
                high = comparisonResult > 0 ? middle - 1 : high;
            }

            //Элемент на последней найденной медиане
            if (collection[high].Equals(value))
                return high;

            return -1;
        }
    }
}
