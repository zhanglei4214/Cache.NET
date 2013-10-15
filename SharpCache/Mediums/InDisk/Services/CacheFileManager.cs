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

    internal class CacheFileManager : ICacheFileManager
    {
        #region Fields

        private const string indexFile = "cache.idx";

        private readonly InDiskCacheDigestSelector digestSelector;

        private readonly FileAllocator allocator;

        #endregion

        #region Constructors

        public CacheFileManager(string cacheDir)
        {
            this.allocator = new FileAllocator(cacheDir);

            this.digestSelector = new InDiskCacheDigestSelector();
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public InDiskCacheDigest GetCacheFileSummary(System.IO.FileStream stream)
        {
            throw new NotImplementedException();
        }

        public CacheValue GetCacheValue(System.IO.FileStream stream)
        {
            throw new NotImplementedException();
        }

        public bool Set(IHashable index, CacheItemMetaData meta, byte[] value)
        {
            InDiskCacheDigest digest = this.digestSelector.Get(index);

            if (digest == null)
            {
                digest = this.digestSelector.Insert(index, value.LongLength);
            }

            return digest.Set(index, meta, value);
        }

        public bool Remove(IHashable index)
        {
            InDiskCacheDigest digest = this.digestSelector.Get(index);
            if (digest == null)
            {
                return true;
            }

            this.digestSelector.Remove(index);

            return true;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
