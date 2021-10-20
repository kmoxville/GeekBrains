using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson6
{
    /// <summary>
    /// Граф реализован через словарь
    /// Ключ - вершина
    /// Ребра в HashSet
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Graph<T> : IEnumerable<(T vertex, IEnumerable<T> edges)>
    {
        private Dictionary<T, HashSet<T>> _graph = new();

        /// <summary>
        /// Пары вершина-ребра
        /// </summary>
        /// <param name="vertices"></param>
        public Graph(IEnumerable<Tuple<T, IEnumerable<T>>> verticesAndEdges)
        {
            foreach ((T vertex, IEnumerable<T> edges) in verticesAndEdges)
            {
                AddVertex(vertex, edges);
            }
        }

        public Graph()
        {
        }

        public void AddVertex(T vertex, IEnumerable<T> edges = null)
        {
            edges ??= new HashSet<T>();
            _graph.Add(vertex, new HashSet<T>(edges));
        }

        public void Add((T vertex, IEnumerable<T> edges) tuple)
        {
            AddVertex(tuple.vertex, tuple.edges);
        }

        public T[] DFS(T start)
        {
            HashSet<T> visited = new();
            Stack<T> stack = new();

            stack.Push(start);
            
            do
            {
                start = stack.Pop();
                visited.Add(start);

                foreach (var neighbor in _graph[start])
                {
                    if (!visited.Contains(neighbor))
                        stack.Push(neighbor);
                }
            } while (stack.Count > 0);

            return visited.ToArray();
        }

        public T[] BFS(T start)
        {
            HashSet<T> visited = new();
            Queue<T> queue = new();

            queue.Enqueue(start);

            do
            {
                start = queue.Dequeue();
                visited.Add(start);

                foreach (var neighbor in _graph[start])
                {
                    if (!visited.Contains(neighbor))
                        queue.Enqueue(neighbor);
                }
            } while (queue.Count > 0);

            return visited.ToArray();
        }

        public IEnumerator<(T vertex, IEnumerable<T> edges)> GetEnumerator()
        {
            foreach (var item in _graph)
            {
                yield return (item.Key, item.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
