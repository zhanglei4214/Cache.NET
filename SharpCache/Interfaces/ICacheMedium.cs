namespace SharpCache.Interfaces
{
    #region Using Directives
    using System;
    using SharpCache.EventArguments;
    using SharpCache.Mediums;
    #endregion

    internal interface ICacheMedium
    {
        string CacheName { get; }

        CacheMediumType Type { get; }

        CacheValue Get(CacheKey key);

        bool Set(CacheKey key, CacheValue value);

        bool Set(CacheItem[] items);

        bool Remove(CacheKey key);

        void Clear();

        string Dump();

        CacheSummary[] Replace();

        ICacheMedium Next();

        ICacheMedium Previous();

        IReplacementAlgorithm ReplacementAlgorithm { get; set; }

        event EventHandler<GetCacheItemEventArgs> GetCacheItemEvent;

        event EventHandler<AddCacheItemEventArgs> AddCacheItemEvent;

        event EventHandler<RemoveCacheItemEventArgs> RemoveCacheItemEvent;

        event EventHandler ClearCacheItemsEvent;
    }
}
