using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson4_2
{
    public class Tree<T> : ITree<T>, IEnumerable<Tree<T>> 
        where T : IComparable<T>
    {
        private Tree<T> _left;
        private Tree<T> _right;
        private Tree<T> _root;

        public Tree(T value)
        {
            if (value == null) throw new ArgumentNullException("Tree(value)");

            Value = value;
        }

        private Tree(T value, Tree<T> root)
            : this(value)
        {
            _root = root;
        }

        public T Value { get; }

        public ITree<T> this[T value]
        {
            get
            {
                foreach (Tree<T> node in this)
                {
                    if (node.Value.CompareTo(value) == 0)
                        return node;
                }

                return null;
            }
        }

        public void Add(T value)
        {
            int cmp = Value.CompareTo(value);

            if (cmp == 0)
            {
                return;
            }
            else if (cmp < 0)
            {
                InitTreeNode(value, ref _right).Add(value);
            }
            else if (cmp > 0)
            {
                InitTreeNode(value, ref _left).Add(value);
            }
        }

        public void TraverseTree(Tree<T> node, Action<Tree<T>> action)
        {
            if (node != null)
            {
                action(node);
                TraverseTree(node._left, action);
                TraverseTree(node._right, action);
            }
        }

        public void DebugPrint()
        {        
            DebugPrint(this, 50, 0, Console.CursorTop + 1);
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.WriteLine('\n');
        }

        private void DebugPrint(Tree<T> node, int center, int depth, int top)
        {
            if (node != null)
            {
                Console.SetCursorPosition(center, depth + top);
                Console.Write(node.Value.ToString());
                DebugPrint(node._left, center - 15 + depth * 2, depth + 2, top);
                DebugPrint(node._right, center + 15 - depth * 2, depth + 2, top);
            }
        }

        public IEnumerator<Tree<T>> GetEnumerator()
        {
            yield return this;

            if (_left != null)
            {
                foreach (var node in _left)
                {
                    yield return node;
                }
            }

            if (_right != null)
            {
                foreach (var node in _right)
                {
                    yield return node;
                }
            }
        }

        public override bool Equals(object obj)
        {
            Tree<T> node = obj as Tree<T>;

            if (node == null)
                return false;

            return this.Value.CompareTo(node.Value) == 0;
        }

        public void Remove(T value)
        {
            Remove(value, false);
        }

        /// <summary>
        /// Добавляет удаленные ноды обратно в дерево, если удаление не рекурсивно
        /// </summary>
        /// <param name="value"></param>
        /// <param name="recursively"></param>
        public void Remove(T value, bool recursively = false)
        {
            Tree<T> removeNode = (Tree<T>)this[value];
            if (removeNode == null) throw new ArgumentOutOfRangeException("Remove");
            if (removeNode == this) throw new InvalidOperationException("cant remove itself, use =null instead");

            List<Tree<T>> nodesToAdd = new();

            if (!recursively)
            {
                foreach (Tree<T> node in removeNode)
                {
                    if (node != removeNode)
                        nodesToAdd.Add(node);
                }
            }

            Tree<T> root = removeNode._root;
            if (root == null)
            {
                removeNode._left = null;
                removeNode._right = null;
                return;
            }

            if (root._left == removeNode)
                root._left = null;
            else if (root._right == removeNode)
                root._right = null;

            foreach (Tree<T> node in nodesToAdd)
            {
                removeNode._root.Add(node.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private Tree<T> InitTreeNode(T value, ref Tree<T> node)
        {
            node ??= new Tree<T>(value, this);
            return node;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
