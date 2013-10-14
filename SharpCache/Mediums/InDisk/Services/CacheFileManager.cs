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

        public bool Set(PathSector sector, object value)
        {
            InDiskCacheDigest digest = this.CreateOrGetCacheFileDigest(sector);

            InDiskCacheItemMetaData meta = this.CreateOrGetCacheItemMetaData(digest, sector.Last);

            //// TODO: manipulate meta 1) meta is not ready, find a new place
            ////                       2) meta is there, space is enough, just set
            ////                       3) meta is there, but space is not enough, find a new place
            throw new NotImplementedException();
        }

        public bool Remove(PathSector sector)
        {
            InDiskCacheDigest digest = this.GetCacheFileDigest(sector);
            if (digest == null)
            {
                return true;
            }

            this.ClearCacheItemMetaData(digest, sector.Last);

            return true;
        }

        #endregion

        #region Private Methods

        private InDiskCacheDigest CreateOrGetCacheFileDigest(PathSector sector)
        {
            InDiskCacheDigest digest = this.GetCacheFileDigest(sector);
            if (digest == null)
            {
                digest = new InDiskCacheDigest(this.allocator);

                digest.Build(sector);

                this.digestSelector[sector] = digest;
            }

            return digest;
        }

        private InDiskCacheDigest GetCacheFileDigest(PathSector sector)
        {
            InDiskCacheDigest digest = null;
            if (this.digestSelector.TryGetValue(sector, out digest) == false)
            {
                digest = null;
            }

            return digest;
        }

        private InDiskCacheItemMetaData CreateOrGetCacheItemMetaData(InDiskCacheDigest digest, long index)
        {
            InDiskCacheItemMetaData meta;
            if (digest.TryGet(index, out meta) == false)
            {
                meta = new InDiskCacheItemMetaData();
            }

            return meta;
        }

        private void UpdateCacheItemMetaData(InDiskCacheDigest digest, long index, long offset)
        {
            throw new NotImplementedException();
        }

        private void ClearCacheItemMetaData(InDiskCacheDigest digest, long index)
        {
            digest.Remove(index);
        }

        #endregion
    }
}
