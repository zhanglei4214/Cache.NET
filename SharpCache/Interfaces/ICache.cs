namespace SharpCache.Interfaces
{
    public interface ICache
    {
        string Name { get; }

        CacheValue this[CacheKey key] { get; set; }

        bool Exists(CacheKey key);

        void Clear();

        void Adjust(CacheConfiguration configuration);

        /// <summary>
        /// For debug usage.
        /// </summary>
        string Dump();
    }
}
