using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson5
{
    public class Tree<T> : IEnumerable<KeyValuePair<int, T>>
        where T : IComparable<T>
    {
        private Dictionary<int, T> _items;

        public Tree()
        {
            _items = new();
        }

        public void Add(T value)
        {
            int i = 1;
            while (true)
            {
                if (!_items.ContainsKey(i))
                {
                    _items.Add(i, value);
                    return;
                }

                int comparisonResult = value.CompareTo(_items[i]);

                if (comparisonResult < 0)
                {
                    i = NodeIndex.GetLeftNode(i);
                }
                else if (comparisonResult > 0)
                {
                    i = NodeIndex.GetRightNode(i);
                }
                else
                {
                    return; //дубль
                }
            }
        }

        public int IndexOf(T value, SearchKind searchKind)
        {
            if (searchKind == SearchKind.BFS)
                return IndexOfBFS(value);
            else if (searchKind == SearchKind.DFS)
                return IndexOfDFS(value);
            else
                return IndexOfFast(value);
        }

        private int IndexOfDFS(T value)
        {
            Queue<int> queue = new();
            GetDFSIndexes(1, queue);

            int currentIndex = 0;
            T currentValue = default(T);
            while (queue.Count != 0)
            {
                currentIndex = queue.Dequeue();
                currentValue = _items[currentIndex];
                Debug.WriteLine($"Current index: {currentIndex}; Current value: {currentValue}");
            }

            if (currentValue.CompareTo(value) != 0)
                return -1;

            return currentIndex;
        }

        private int IndexOfBFS(T value)
        {
            int currentIndex = 0;
            T currentValue = default(T);
            foreach (var pair in _items.OrderBy(a => a.Key))
            {
                currentIndex = pair.Key;
                currentValue = pair.Value;
                Debug.WriteLine($"Current index: {currentIndex}; Current value: {currentValue}");
            }

            if (currentValue.CompareTo(value) != 0)
                return -1;

            return currentIndex;
        }

        private int IndexOfFast(T value)
        {
            throw new NotImplementedException();
        }

        private void GetDFSIndexes(int index, Queue<int> indexes)
        {
            if (_items.ContainsKey(index))
            {
                indexes.Enqueue(index);
                GetDFSIndexes(NodeIndex.GetLeftNode(index), indexes);
                GetDFSIndexes(NodeIndex.GetRightNode(index), indexes);
            }
        }

        public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void DebugPrint()
        {
            DebugPrint(1, 50, 0, Console.CursorTop + 1);
            Console.SetCursorPosition(0, Console.CursorTop + 2);
            Console.WriteLine("\n\n");
        }

        private void DebugPrint(int index, int center, int depth, int top)
        {
            if (_items.ContainsKey(index))
            {
                Console.SetCursorPosition(center, depth + top);
                Console.Write(_items[index].ToString());
                DebugPrint(NodeIndex.GetLeftNode(index), center - 15 + depth * 2, depth + 2, top);
                DebugPrint(NodeIndex.GetRightNode(index), center + 15 - depth * 2, depth + 2, top);
            }
        }

        /// <summary>
        /// вспомогательный класс, вычисляет индексы нод
        /// </summary>
        private static class NodeIndex
        {
            private static Dictionary<int, int> _cachedFirstItemsInRow = new();

            public static int GetLeftNode(int current)
            {
                int firstItemInRow;
                if (!_cachedFirstItemsInRow.TryGetValue(current, out firstItemInRow))
                {
                    int n = current;

                    n--;
                    n |= n >> 1;
                    n |= n >> 2;
                    n |= n >> 4;
                    n |= n >> 8;
                    n |= n >> 16;
                    n++;

                    firstItemInRow = current % 2 == 0 ? n : (n >> 1);
                    _cachedFirstItemsInRow.Add(current, firstItemInRow);
                }

                return firstItemInRow * 2 + (current - firstItemInRow) * 2;
            }

            public static int GetRightNode(int current)
            {
                return GetLeftNode(current) + 1;
            }

            public static int GetRootNode(int current)
            {
                return GetLeftNode(current) / 2;
            }
        }    
    }

    public enum SearchKind
    {
        BFS,
        DFS,
        Fast
    }
}
