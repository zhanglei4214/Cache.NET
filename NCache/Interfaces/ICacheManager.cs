namespace NCache.Interfaces
{
    public interface ICacheManager
    {
        ICache this[string name] { get; }

        ICache[] Caches { get; }

        ICache Contains(string name);

        ICache Create(string name);

        ICache Create(string name, CacheConfiguration configuration);

        bool Remove(string name);
    }
}
