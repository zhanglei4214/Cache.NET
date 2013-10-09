namespace SharpCache.Interfaces
{
    #region Using Directives
    #endregion

    public interface IReplacementAlgorithm
    {
        CacheSummary[] Replace(IReplaceableCache cache);

        void Add(CacheSummary[] summarys, IReplaceableCache cache);

        void Get(CacheKey key, IReplaceableCache cache);

        void Remove(CacheKey key, IReplaceableCache cache);
    }
}
