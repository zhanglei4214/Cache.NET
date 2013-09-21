namespace SharpCache.Common.DataStructures
{
    #region Using Directives
    using System;
    using SharpCache.Common.Interfaces;
    #endregion

    public class DoubleLinkList<T> : IDoubleLinkList<T>
    {
        #region Fields

        Node<T> head;

        Node<T> tail;

        int size = 0;

        #endregion

        #region Properties

        public Node<T> Head
        {
            get
            {
                return this.head;
            }
        }

        public Node<T> Tail
        {
            get
            {
                return this.tail;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.head == null;
            }
        }

        public int Count
        {
            get
            {
                return this.size;
            }
        }

        #endregion

        #region Public Methods

        public void AddFirst(T t)
        {
            Node<T> node = new Node<T>(t);

            if (this.head == null)
            {
                this.head = node;
                this.tail = node;
                this.size++;

                return;
            }

            this.head.Prev = node;
            node.Next = this.head;
            this.head = node;

            this.size++;
        }

        public void AddLast(T t)
        {
            Node<T> node = new Node<T>(t);

            if (this.head == null)
            {
                this.head = node;
                this.tail = node;
                this.size++;
                return;
            }

            this.tail.Next = node;
            node.Prev = this.tail;
            this.tail = node;

            this.size++;
        }

        public void Insert(int index, T t)
        {
            Node<T> node = new Node<T>(t);

            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            if (this.IsEmpty && index > 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                this.AddFirst(t);
                return;
            }

            Node<T> current = this.head;
            int i = 0;
            while (true)
            {
                if (i == index)
                {
                    break;
                }

                i++;
                current = current.Next;
            }

            current.Prev.Next = node;
            node.Prev = current.Prev;
            node.Next = current;
            current.Prev = node;

            this.size++;
        }

        public void Remove(Node<T> node)
        {
            if (node == this.head)
            {
                this.RemoveFirst();
            }

            else if (node == this.tail)
            {
                this.RemoveLast();
            }
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }

            this.size--;
        }

        public void MoveToHead(Node<T> node)
        {
            if (this.head == null)
            {
                this.head = node;
                this.tail = node;
            }
            else
            {
                this.head.Prev = node;
                node.Next = this.Head;
                this.head = node;
            }
        }

        public void RemoveAt(int index)
        {
            if (this.IsEmpty)
            {
                throw new Exception("list is empty.");
            }

            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                this.RemoveFirst();
                return;
            }

            if (index == size - 1)
            {
                this.RemoveLast();
                return;
            }

            Node<T> current = head;
            int i = 0;
            while (true)
            {
                if (i == index)
                {
                    break;
                }

                i++;
                current = current.Next;
            }

            current.Prev.Next = current.Next;
            current.Next.Prev = current.Prev;

            size--;
        }

        public void RemoveFirst()
        {
            if (this.IsEmpty)
            {
                throw new Exception("list is empty.");
            }

            if (size == 1)
            {
                this.Clear();
                return;
            }

            this.head = this.head.Next;
            this.head.Prev = null;

            this.size--;
        }

        public void RemoveLast()
        {
            if (this.IsEmpty)
            {
                throw new Exception("list is empty.");
            }

            if (this.size == 1)
            {
                this.Clear();
                return;
            }

            this.tail = this.tail.Prev;
            this.tail.Next = null;

            this.size--;
        }

        public void Clear()
        {
            this.head = null;
            this.tail = null;
            this.size = 0;
        }

        public Node<T> this[long index]
        {
            get
            {
                if (this.head == null)
                {
                    throw new Exception("list is empty.");
                }

                if (index >= this.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                Node<T> current = new Node<T>();

                if (index < this.size / 2)
                {
                    current = this.head;
                    int i = 0;
                    while (true)
                    {
                        if (i == index)
                        {
                            break;
                        }

                        current = current.Next;
                        i++;
                    }

                    return current;
                }
                else
                {
                    current = this.tail;
                    int i = this.size;
                    while (true)
                    {
                        if (i == index)
                        {
                            break;
                        }

                        current = current.Prev;
                        i--;
                    }

                    return current.Next;
                }
            }
        }

        #endregion
    }
}
