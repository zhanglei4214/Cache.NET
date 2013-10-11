namespace SharpCache.Mediums.InDisk
{
    #region Using Directives
    using System.IO;
    using SharpCache.Interfaces;
    #endregion

    public interface ICacheFileManager
    {
        /// <summary>
        /// FileIndex is used to keep the meta data of InDiskCache.
        /// </summary>
        ICache FileIndex { get; }

        FileStream FileStream { get; }

        bool Remove(CacheKey key);

        bool Set(CacheItem[] items);

        string AnalyzeCacheFile(string path);

        FileSummary GetCacheFileSummary(FileStream stream);

        CacheValue GetCacheValue(FileStream stream);
    }
}
