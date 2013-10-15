﻿namespace SharpCache.Mediums.InDisk.Interfaces
{
    #region Using Directives
    using System;
    using System.IO;
    using SharpCache.Interfaces;
    using SharpCache.Mediums.InDisk.DataStructures;
    #endregion

    internal interface ICacheFileManager : IDisposable
    {
        bool Remove(IHashable index);

        bool Set(IHashable index, CacheItemMetaData meta, byte[] value);

        InDiskCacheDigest GetCacheFileSummary(FileStream stream);

        CacheValue GetCacheValue(FileStream stream);
    }
}
