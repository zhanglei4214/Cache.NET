namespace SharpCache.Mediums.InDisk
{
    #region Using Directives
    #endregion

    internal class FileAllocator
    {
        #region Constructors

        private FileAllocator()
        {
        }

        #endregion

        #region Public Methods

        public static CacheFileSector Parse(CacheKey key)
        {
            return new CacheFileSector(0, 0, 0);
        }

        #endregion
    }
}
