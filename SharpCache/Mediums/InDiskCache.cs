﻿namespace SharpCache.Mediums
{
    #region Using Directives
    using System;
    using Microsoft.Practices.Prism.Logging;
    #endregion

    internal class InDiskCache : CacheMediumBase
    {
        #region Constructors

        public InDiskCache(string name,CacheCapacity capacity, ILoggerFacade logger)
            : base(name, capacity, logger)
        {
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
