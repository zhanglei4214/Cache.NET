namespace NCache.Common.Interfaces
{
    #region Using Directives
    using System;
    using NCache.Common.DataStructures;
    #endregion

    public interface IDoubleLinkList<T>
    {
        void AddFirst(T t);

        void AddLast(T t);

        void Clear();

        int Count { get; }

        Node<T> Head { get; }

        Node<T> Tail { get; }

        void Insert(int index, T t);

        bool IsEmpty { get; }

        void RemoveAt(int index);

        void Remove(Node<T> node);

        void MoveToHead(Node<T> node);

        void RemoveFirst();

        void RemoveLast();

        Node<T> this[long index] { get; }
    }
}
