namespace SharpCache.Interfaces
{
    internal interface ICacheScheduler
    {
        int CacheLevel();

        CacheValue Get(CacheKey key);

        bool Set(CacheKey key, CacheValue value);

        bool Remove(CacheKey key);

        bool Exists(CacheKey key);

        void Clear();

        void Adjust(SchedulerConfiguration configuration);

        string Dump();
    }
}
