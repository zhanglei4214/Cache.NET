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

        public string AnalyzeCacheFile(string path)
        {
            throw new NotImplementedException();
        }

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

            InDiskCacheItemDigest item = this.CreateOrGetCacheItemDigest(digest, sector.Stub);

            //// TODO: manipulate item 1) item is not ready, find a new place
            ////                       2) item is there, space is enough, just set
            ////                       3) item is there, but space is not enough, find a new place
            throw new NotImplementedException();
        }

        public bool Remove(PathSector sector)
        {
            InDiskCacheDigest digest = this.GetCacheFileDigest(sector);
            if (digest == null)
            {
                return true;
            }

            this.ClearCacheItemDigest(digest, sector.Stub);

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

        private InDiskCacheItemDigest CreateOrGetCacheItemDigest(InDiskCacheDigest digest, long index)
        {
            InDiskCacheItemDigest info;
            if (digest.TryGet(index, out info) == false)
            {
                info = new InDiskCacheItemDigest();
            }

            return info;
        }

        private void UpdateCacheItemDigest(InDiskCacheDigest digest, long index, long offset)
        {
            throw new NotImplementedException();
        }

        private void ClearCacheItemDigest(InDiskCacheDigest digest, long index)
        {
            this.UpdateCacheItemDigest(digest, index, 0);
        }

        #endregion
    }
}
