namespace NCache.Interfaces
{
    #region Using Directives
    using NCache.Algorithms;
    #endregion

    public interface IReplaceableCache
    {
        long Count();

        long MaxCount();

        long CacheSize();

        AlgorithmData AlgorithmData { get; }
    }
}
