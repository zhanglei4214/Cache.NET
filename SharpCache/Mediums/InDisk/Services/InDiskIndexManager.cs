namespace SharpCache.Mediums.InDisk.Services
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using SharpCache.Interfaces;
    using SharpCache.Mediums.InDisk.DataStructures;
    #endregion

    internal class InDiskIndexManager
    {
        #region Fields

        private readonly int capacity;

        private Dictionary<IHashable, LinkedListNode<InDiskIndex>> inUseIndexInDict;

        private LinkedList<InDiskIndex> inUseIndexInList;

        private LinkedList<InDiskIndex> emptyIndexInList;

        #endregion

        #region Constructors

        public InDiskIndexManager(int capacity)
        {
            this.capacity = capacity;

            this.inUseIndexInDict = new Dictionary<IHashable, LinkedListNode<InDiskIndex>>();

            this.inUseIndexInList = new LinkedList<InDiskIndex>();

            this.emptyIndexInList = new LinkedList<InDiskIndex>();
        }

        #endregion

        #region Public Methods

        public InDiskIndex FindFree(IHashable key)
        {
            //// TODO: this is a wrong logic.
            return this.inUseIndexInDict[key].Value;
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
