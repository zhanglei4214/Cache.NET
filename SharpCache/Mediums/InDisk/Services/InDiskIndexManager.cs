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

        private BlockManager usedManager;

        private BlockManager emptyManager;

        #endregion

        #region Constructors

        public InDiskIndexManager()
        {
            this.usedManager = new BlockManager();

            this.emptyManager = new BlockManager();
        }

        #endregion

        #region Public Methods

        public int FindFree()
        {
            int index = this.emptyManager.First();
            this.usedManager.Set(index);

            return index;
        }

        public bool TryGet(long index, out int meta)
        {
            throw new NotImplementedException();
        }

        public bool TrySet(long index, out int meta)
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
