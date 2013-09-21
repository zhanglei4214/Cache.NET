namespace NCache.Mediums
{
    #region Using Directives
    using System;
    #endregion

    internal class FileCache : CacheMediumBase
    {
        #region Constructors

        public FileCache(string name,CacheCapacity capacity)
            : base(name, capacity)
        {
        }

        #endregion

        #region Protected Methods

        protected override CacheMediumType CacheMediumType()
        {
            return Mediums.CacheMediumType.File;
        }

        protected override CacheValue DoGet(CacheKey key)
        {
            throw new NotImplementedException();
        }

        protected override bool DoSet(CacheItem[] items)
        {
            throw new NotImplementedException();
        }

        protected override bool DoRemove(CacheKey key)
        {
            throw new NotImplementedException();
        }

        protected override void DoClear()
        {
            // TODO : Delete the files.
        }

        protected override string DoDump()
        {
            throw new NotImplementedException();
        }

        protected override long DoCount()
        {
            throw new NotImplementedException();
        }

        protected override long DoGetCacheSize()
        {
            throw new NotImplementedException();
        }

        protected override long DoMaxCount()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
