namespace SharpCache.Mediums
{
    #region Using Directives
    using System;
    using System.IO;
    using Microsoft.Practices.Prism.Logging;
    using SharpCache.Common;
    using SharpCache.Mediums.InDisk.DataStructures;
    using SharpCache.Mediums.InDisk.Interfaces;
    using SharpCache.Mediums.InDisk.Services;
    #endregion

    internal class InDiskCache : CacheMediumBase
    {
        #region Fields

        private readonly ICacheFileManager fileManager;

        #endregion

        #region Constructors

        public InDiskCache(string name,CacheCapacity capacity, ILoggerFacade logger)
            : base(name, capacity, logger)
        {
            this.fileManager = new CacheFileManager(Environment.CurrentDirectory);
        }

        #endregion

        #region Properties

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
                return this.fileManager.Set(item.Key, item.Value.MetaData, item.Value.Serialize());
            }

            return true;
        }

        protected override bool DoRemove(CacheKey key)
        {            
            return this.fileManager.Remove(key);
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
