namespace SharpCache.Common.DataStructures
{
    #region Using Directives
    using System;
    #endregion

    public class Node<T>
    {
        #region Fields

        T data;

        Node<T> next;

        Node<T> prev;

        #endregion

        #region Constructors

        public Node()
        {
            this.data = default(T);
            this.next = null;
            this.prev = null;
        }

        public Node(T t)
        {
            this.data = t;
            this.next = null;
            this.prev = null;
        }

        public Node(T t, Node<T> next, Node<T> prev)
        {
            this.data = t;
            this.next = next;
            this.prev = prev;
        }

        #endregion

        #region Properties

        public T Data
        {
            get
            {
                return this.data;
            }

            set
            {
                this.data = value;
            }
        }

        public Node<T> Next
        {
            get
            {
                return this.next;
            }

            set
            {
                this.next = value;
            }
        }

        public Node<T> Prev
        {
            get
            {
                return prev;
            }

            set
            {
                prev = value;
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            T p = this.prev == null ? default(T) : this.prev.data;
            T n = this.next == null ? default(T) : this.next.data;
            string s = string.Format("Data:{0},Prev:{1},Next:{2}", data, p, n);
            return s;
        }

        #endregion
    }
}
