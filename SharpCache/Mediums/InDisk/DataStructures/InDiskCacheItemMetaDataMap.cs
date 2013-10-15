namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    using System;
    using SharpCache.Interfaces;
    #endregion

    internal class InDiskCacheItemMetaDataMap
    {
        #region Constructors

        public InDiskCacheItemMetaDataMap()
        {
        }

        #endregion

        #region Public Methods

        public InDiskCacheItemMetaData FindFree(IHashable index)
        {
            throw new NotImplementedException();
        }

        public bool TryGet(long index, out InDiskCacheItemMetaData meta)
        {
            throw new NotImplementedException();
        }

        public bool TrySet(long index, out InDiskCacheItemMetaData meta)
        {
            throw new NotImplementedException();
        }

        public void Remove(long index)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
