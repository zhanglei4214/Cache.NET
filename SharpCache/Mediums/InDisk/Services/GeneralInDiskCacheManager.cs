namespace SharpCache.Mediums.InDisk.Services
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using SharpCache.Config;
    using SharpCache.Interfaces;
    using SharpCache.Mediums.InDisk.DataStructures;
    using SharpCache.Mediums.InDisk.Interfaces;
    using SharpCache.Common;
    #endregion

    internal class GeneralInDiskCacheManager : IInDiskCacheManager
    {
        #region Fields

        private readonly InDiskCacheSelector selector;       

        #endregion

        #region Constructors

        public GeneralInDiskCacheManager(string cacheDir)
        {            
            this.selector = new InDiskCacheSelector(cacheDir);
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public bool Set(IHashable key, CacheItemMetaData meta, byte[] value)
        {
            IInDiskCacheManager cacheManager = this.selector.Get(key);

            if (cacheManager == null)
            {
                cacheManager = this.selector.AddNew(key, value.Length);
            }

            return cacheManager.Set(key, meta, value);
        }

        public bool Remove(IHashable index)
        {
            IInDiskCacheManager cacheManager = this.selector.Get(index);
            if (cacheManager == null)
            {
                return true;
            }

            this.selector.Remove(index);

            return true;
        }

        public void Dispose()
        {
            this.selector.Dispose();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
