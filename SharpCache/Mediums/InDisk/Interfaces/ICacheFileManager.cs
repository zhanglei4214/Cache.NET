namespace SharpCache.Mediums.InDisk.Interfaces
{
    #region Using Directives
    using System.IO;
    using SharpCache.Interfaces;
    using SharpCache.Mediums.InDisk.DataStructures;
    #endregion

    internal interface ICacheFileManager
    {
        /// <summary>
        /// FileIndex is used to keep the meta data of InDiskCache.
        /// </summary>
        ICache FileIndex { get; }

        FileStream FileStream { get; }

        bool Remove(CacheKey key);

        bool Set(PathSector sector, object value);

        string AnalyzeCacheFile(string path);

        FileSummary GetCacheFileSummary(FileStream stream);

        CacheValue GetCacheValue(FileStream stream);
    }
}
