namespace NCache.Algorithms
{
    #region Using Directives
    using NCache.Interfaces;
    #endregion

    public class LRUReplacementAlgorithm : IReplacementAlgorithm
    {
        #region Constructors

        public LRUReplacementAlgorithm()
        {
        }

        #endregion

        #region Public Methods

        public CacheKey[] Replace(IReplaceableCache cache)
        {
            CacheKey[] items;

            // cache does not exceed maximum count, do not replace
            if (cache.Count() <= cache.MaxCount())
            {
                return null;
            }
            else
            {
                // cache count exceeds max entries, remove from the end of queue
                int itemCountToReplace = (int) (cache.Count() - cache.MaxCount());

                items = new CacheKey[itemCountToReplace];

                for (int i = itemCountToReplace - 1; i >= 0; i--)
                {
                    items[i] = cache.AlgorithmData.RemoveFromTail();
                }
            }

            // to do: remove cache entries has been expired
            return items;
        }

        public void Add(CacheSummary[] summarys, IReplaceableCache cache)
        {
            if (summarys == null || cache == null)
            {
                return;
            }

            foreach (CacheSummary summary in summarys)
            {
                cache.AlgorithmData.AddToHead(summary);
            }
        }

        public void Get(CacheKey key, IReplaceableCache cache)
        {
            if (cache.AlgorithmData != null && key != null)
            {
                cache.AlgorithmData.MoveToHead(key);
            }
        }

        public void Remove(CacheKey key, IReplaceableCache cache)
        {
            if (cache.AlgorithmData != null && key != null)
            {
                cache.AlgorithmData.Remove(key);
            }
        }

        #endregion
    }
}
