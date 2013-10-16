namespace SharpCache.Mediums.InDisk.Services
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using SharpCache.Interfaces;
    using SharpCache.Mediums.InDisk.DataStructures;
    #endregion

    internal class InDiskIndexOperator
    {
        #region Fields

        private readonly int capacity;

        private Dictionary<IHashable, LinkedListNode<InDiskIndex>> digestInDict;

        private LinkedList<InDiskIndex> digestInList;

        #endregion

        #region Constructors

        public InDiskIndexOperator(int capacity)
        {
            this.capacity = capacity;

            this.digestInDict = new Dictionary<IHashable, LinkedListNode<InDiskIndex>>();
            this.digestInList = new LinkedList<InDiskIndex>();
        }

        #endregion

        #region Public Methods

        public InDiskIndex FindFree(IHashable key)
        {
            //// TODO: this is a wrong logic.
            return this.digestInDict[key].Value;
        }

        public bool TryGet(long index, out InDiskIndex meta)
        {
            throw new NotImplementedException();
        }

        public bool TrySet(long index, out InDiskIndex meta)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IHashable key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
