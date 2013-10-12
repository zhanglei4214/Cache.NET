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

        private const int SMALL_FILE_SIZE = 2 * 1024 * 1024;

        private const int BIG_FILE_SIZE = 10 * SMALL_FILE_SIZE;

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
                PathSector sector = FileAllocator.Parse(item.Key);

                return this.fileManager.Set(sector, item.Value);
            }

            return true;
        }

        protected override bool DoRemove(CacheKey key)
        {
            PathSector sector = FileAllocator.Parse(key);

            return this.fileManager.Remove(sector);
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
