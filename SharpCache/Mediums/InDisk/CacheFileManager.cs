namespace SharpCache.Mediums.InDisk
{
    #region Using Directives
    using System;
    #endregion

    public class CacheFileManager : ICacheFileManager
    {
        #region Constructors

        public CacheFileManager()
        { 
        }

        #endregion

        #region Public Methods

        public System.IO.FileStream FileStream
        {
            get { throw new NotImplementedException(); }
        }

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

        #endregion
    }
}
