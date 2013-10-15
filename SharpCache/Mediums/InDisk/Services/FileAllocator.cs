namespace SharpCache.Mediums.InDisk.Services
{
    #region Using Directives
    using System.IO;
    using SharpCache.Common;
    using SharpCache.Mediums.InDisk.DataStructures;
    using System;
    #endregion

    internal class FileAllocator
    {
        #region Fields

        private readonly string cacheDirBase;

        private const string IN_DISK_CACHE = "IN_DISK_CACHE";

        private const string CACHE_STUB = "stub";

        #endregion

        #region Constructors

        public FileAllocator(string cacheDir)
        {
            this.cacheDirBase = Path.Combine(cacheDir, IN_DISK_CACHE);
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private static bool Divide(long value, out long first, out long second, out long third)
        {
            first = 0;
            second = 0;
            third = 0;

            first = value & 0x3;

            second = (value >> 2) & 0xF;

            third = (int)(value >> 6);

            return true;
        }

        #endregion
    }
}
