namespace SharpCache.Interfaces
{
    #region Using Directives
    using SharpCache.Common.DataStructures;
    #endregion

    public interface IReplacementAlgorithm
    {
        CacheKey[] Replace(IReplaceableCache cache);

        void Add(CacheSummary[] summarys, IReplaceableCache cache);

        void Get(CacheKey key, IReplaceableCache cache);

        void Remove(CacheKey key, IReplaceableCache cache);
    }
}
