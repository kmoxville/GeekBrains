using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AlgLesson2
{
    /// <summary>
    /// Названия некоторых методов отличаются от тз, но смысл тот же
    /// AddNode/AddNodeAfter можно имулировать Add/Insert
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedList<T> : ILinkedList<T>
    {
        private int _size;
        private Node<T> _first;
        private Node<T> _last;

        public T this[int index] 
        { 
            get => GetNode(index).Value; 
            set
            {
                Node<T> node = GetNode(index);
                node.Value = value;
            }
        }

        public int Count => _size;

        public void Add(T value)
        {
            if (_last == null)
            {
                _last = new Node<T>() { Value = value };
                _first = _last;

                return;
            }

            Node<T> newNode = new Node<T>() { Value = value, Prev = _last };
            _last.Next = newNode;
            _last = newNode;

            _size++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currentNode = _first;
            
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode?.Next;
            }
        }

        public int IndexOf(T value)
        {
            int index = -1, counter = 0;

            foreach (var nodeValue in this)
            {
                if (nodeValue.Equals(value))
                {
                    index = counter;
                    break;
                }

                counter++;
            }

            return index;
        }

        public void Insert(int index, T value)
        {
            Node<T> currentNode = GetNode(index);
            Node<T> newNode = new() { Value = value };

            newNode.Next = currentNode;
            newNode.Prev = currentNode?.Prev;

            currentNode?.Prev?.SetNext(newNode);
            currentNode?.SetPrev(newNode);

            _size++;
        }

        public void RemoveAt(int index)
        {
            Node<T> node = GetNode(index);
            if (node == null) throw new ArgumentOutOfRangeException($"{this.ToString()} : RemoveAt()");

            RemoveNodeAt(node);
        }

        public void Clear()
        {
            Node<T> currentNode = _last;

            while (_last != null)
                RemoveNodeAt(_last);

            _size = 0;
        }

        private void RemoveNodeAt(Node<T> node)
        {
            Node<T> prevNode = node?.Prev;
            Node<T> nextNode = node?.Next;

            prevNode?.SetNext(nextNode);
            nextNode?.SetPrev(prevNode);

            _size--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private Node<T> GetNode(int index)
        {
            if (index > _size) throw new ArgumentOutOfRangeException($"{this.ToString()} : GetNode()");

            Node<T> currentNode = _first;

            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode?.Next;
            }

            return currentNode;
        }

        private class Node<TValue>
        {
            public TValue Value { get; set; }
            public Node<TValue> Next { get; set; }
            public Node<TValue> Prev { get; set; }

            //нужны для сокращенного обращения T?.SetNext(), со свойствами это не работает
            public void SetNext(Node<TValue> node) => Next = node;
            public void SetPrev(Node<TValue> node) => Prev = node;
        }
    }
}
