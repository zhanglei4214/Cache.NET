namespace NCache.Interfaces
{
    #region Using Directives
    using NCache.Common.DataStructures;
    #endregion

    public interface IReplacementAlgorithm
    {
        CacheKey[] Replace(IReplaceableCache cache);

        void Add(CacheSummary[] summarys, IReplaceableCache cache);

        void Get(CacheKey key, IReplaceableCache cache);

        void Remove(CacheKey key, IReplaceableCache cache);
    }
}
