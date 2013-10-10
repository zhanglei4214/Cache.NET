namespace SharpCache.Mediums.InDisk
{
    #region Using Directives
    using System.IO;
    #endregion

    public interface ICacheFileManager
    {
        FileStream FileStream { get; }

        string AnalyzeCacheFile(string path);

        FileSummary GetCacheFileSummary(FileStream stream);

        CacheValue GetCacheValue(FileStream stream);
    }
}
