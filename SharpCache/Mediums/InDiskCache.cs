namespace SharpCache.Mediums
{
    #region Using Directives
    using System;
    using Microsoft.Practices.Prism.Logging;
    using SharpCache.Mediums.InDisk.Interfaces;
    using SharpCache.Mediums.InDisk.Services;
    #endregion

    internal class InDiskCache : CacheMediumBase, IDisposable
    {
        #region Fields

        private readonly IInDiskCacheManager cacheManager;

        #endregion

        #region Constructors

        public InDiskCache(string name,CacheCapacity capacity, ILoggerFacade logger)
            : base(name, capacity, logger)
        {
            this.cacheManager = new GeneralInDiskCacheManager(Environment.CurrentDirectory);
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public void Dispose()
        {
            this.cacheManager.Dispose();
        }

        #endregion

        #region Protected Methods

        protected override CacheMediumType CacheMediumType()
        {
            return Mediums.CacheMediumType.InDisk;
        }

        protected override CacheValue DoGet(CacheKey key)
        {
            throw new NotImplementedException();
        }

        protected override bool DoSet(CacheItem[] items)
        {
            foreach (CacheItem item in items)
            {
                return this.cacheManager.Set(item.Key, item.Value.MetaData, item.Value.Serialize());
            }

            return true;
        }

        protected override bool DoRemove(CacheKey key)
        {            
            return this.cacheManager.Remove(key);
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

        #region Private Methods

        #endregion
    }
}
