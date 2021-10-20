using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson6
{
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

        public void AddVertex(T vertex, IEnumerable<T> edges = null)
        {
            edges ??= new HashSet<T>();
            _graph.Add(vertex, new HashSet<T>(edges));
        }

        public void Add((T vertex, IEnumerable<T> edges) tuple)
        {
            AddVertex(tuple.vertex, tuple.edges);
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
