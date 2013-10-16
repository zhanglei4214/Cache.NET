namespace SharpCache.Mediums.InDisk.Interfaces
{
    #region Using Directives
    using System;
    using System.IO;
    using SharpCache.Interfaces;
    using SharpCache.Mediums.InDisk.DataStructures;
    #endregion

    internal interface IInDiskCacheManager : IDisposable
    {
        bool Remove(IHashable key);

        bool Set(IHashable key, CacheItemMetaData meta, byte[] value);
    }
}
