namespace SharpCache.Mediums.InDisk.Interfaces
{
    #region Using Directives
    using System.IO;
    using SharpCache.Interfaces;
    using SharpCache.Mediums.InDisk.DataStructures;
    #endregion

    internal interface ICacheFileManager
    {
        bool Remove(PathSector sector);

        bool Set(PathSector sector, object value);

        string AnalyzeCacheFile(string path);

        InDiskCacheDigest GetCacheFileSummary(FileStream stream);

        CacheValue GetCacheValue(FileStream stream);
    }
}
