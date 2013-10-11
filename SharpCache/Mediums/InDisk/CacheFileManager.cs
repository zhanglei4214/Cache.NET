namespace SharpCache.Mediums.InDisk
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using SharpCache.Config;
    using SharpCache.Interfaces;
    #endregion

    public class CacheFileManager : ICacheFileManager
    {
        #region Fields

        private const string indexFile = "cache.idx";

        private readonly ICache fileIndex;

        private readonly Dictionary<int, string> fileDict;

        #endregion

        #region Constructors

        public CacheFileManager()
        {
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

        public bool Set(CacheItem[] items)
        {
            throw new NotImplementedException();
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
