using System;
using System.Collections.Generic;
using System.Text;

namespace AlgLesson2
{
    //Начальную и конечную ноду нужно хранить в самой реализации интерфейса
    public interface ILinkedList<T> : IEnumerable<T>
    {
        int Count { get; } // возвращает количество элементов в списке

        void Add(T value);  // добавляет новый элемент списка

        void Insert(int index, T value); // добавляет новый элемент списка после определённого элемента

        void RemoveAt(int index); // удаляет элемент по порядковому номеру

        int IndexOf(T value); // ищет элемент по его значению

        T this[int index] { get; set; }
    }
}
