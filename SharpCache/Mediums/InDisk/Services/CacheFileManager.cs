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
    #endregion

    internal class CacheFileManager : ICacheFileManager
    {
        #region Fields

        private const string indexFile = "cache.idx";

        private readonly ICache fileIndex;

        private readonly FileAllocator allocator;

        private readonly Dictionary<int, string> fileDict;

        #endregion

        #region Constructors

        public CacheFileManager(string cacheDir)
        {
            this.allocator = new FileAllocator(cacheDir);

            this.fileIndex = new Cache("__IN_DISK_INDEX_CACHE__", StandardInMemoryConfiguration.Create());

            this.fileDict = new Dictionary<int, string>();
        }

        #endregion

        #region Properties

        public ICache FileIndex
        {
            get
            {
                return this.fileIndex;
            }
        }

        public FileStream FileStream
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Public Methods

        public string AnalyzeCacheFile(string path)
        {
            throw new NotImplementedException();
        }

        public FileSummary GetCacheFileSummary(System.IO.FileStream stream)
        {
            throw new NotImplementedException();
        }

        public CacheValue GetCacheValue(System.IO.FileStream stream)
        {
            throw new NotImplementedException();
        }

        public bool Set(PathSector sector, object value)
        {
            FileStream stream = this.allocator.Find(sector);

            throw new NotSupportedException();
        }

        public bool Remove(CacheKey key)
        {
            CacheValue value = this.FileIndex[key];
            if (value == null)
            {
                return true;
            }

            this.FileIndex[key] = null;

            return true;
        }

        #endregion
    }
}
