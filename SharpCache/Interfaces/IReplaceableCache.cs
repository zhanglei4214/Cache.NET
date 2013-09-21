namespace SharpCache.Interfaces
{
    #region Using Directives
    using SharpCache.Algorithms;
    #endregion

    public interface IReplaceableCache
    {
        long Count();

        long MaxCount();

        long CacheSize();

        AlgorithmData AlgorithmData { get; }
    }
}
