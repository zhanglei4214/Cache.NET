namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    using System;
    #endregion

    internal class InDiskCacheItemMetaDataMap
    {
        #region Constructors

        public InDiskCacheItemMetaDataMap()
        {
        }

        #endregion

        #region Public Methods

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
