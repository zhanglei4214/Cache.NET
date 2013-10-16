namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using SharpCache.Interfaces;
    using SharpCache.Mediums.InDisk.Interfaces;
    #endregion

    internal class InDiskCacheSelector : IDisposable
    {
        #region Fields

        private readonly Dictionary<IHashable, InDiskCacheType> router;

        private readonly Dictionary<InDiskCacheType, IInDiskCacheManager> cacheManagerDict;

        private readonly string cacheDir;

        #endregion

        #region Constructors

        public InDiskCacheSelector(string cacheDir)
        {
            this.cacheDir = Path.Combine(cacheDir, "IN_DISK_CACHE");

            this.router = new Dictionary<IHashable,InDiskCacheType>();

            this.cacheManagerDict = new Dictionary<InDiskCacheType, IInDiskCacheManager>();
        }

        #endregion

        #region Properties

        public string CacheDirectory
        {
            get
            {
                return this.cacheDir;
            }
        }

        #endregion

        #region Public Methods

        public bool Remove(IHashable key)
        {
            return this.router.Remove(key);
        }

        public IInDiskCacheManager Get(IHashable key)
        {
            InDiskCacheType type;
            if (this.router.TryGetValue(key, out type) == true)
            {
                return this.cacheManagerDict[type];
            }

            return null;
        }

        public IInDiskCacheManager AddNew(IHashable key, int length)
        {
            IInDiskCacheManager cacheManager;
            InDiskCacheType type = this.CalculateCacheType(length);
            if (this.cacheManagerDict.ContainsKey(type) == false)
            {
                cacheManager = this.CreateInDiskCache(type);

                this.cacheManagerDict[type] = cacheManager;
            }
            else
            {
                cacheManager = this.cacheManagerDict[type];
            }

            this.router[key] = type;

            return cacheManager;
        }

        public void Dispose()
        {
            foreach (SingleInDiskCacheManager digest in this.cacheManagerDict.Values)
            {
                digest.Dispose();
            }

            this.cacheManagerDict.Clear();

            this.router.Clear();
        }

        #endregion

        private InDiskCacheType CalculateCacheType(int length)
        {
            int current = 0;
            foreach (int size in Enum.GetValues(typeof(InDiskCacheType)))
            {
                if (length < size)
                {       
                    current = size;
                    break;
                }
            }

            switch(current)
            {
                case 16:
                    return InDiskCacheType._16;
                case 32:
                    return InDiskCacheType._32;
                case 64:
                    return InDiskCacheType._64;
                case 128:
                    return InDiskCacheType._128;
                case 256: 
                    return InDiskCacheType._256;
                case 512:
                    return InDiskCacheType._512;
                case 1024:
                    return InDiskCacheType._1K;
                case 2 * 1024:
                    return InDiskCacheType._2K;
                case 4 * 1024:
                    return InDiskCacheType._4K;
                case 8 * 1024:
                    return InDiskCacheType._8K;
                case 16 * 1024:
                    return InDiskCacheType._16K;
                case 32 * 1024:
                    return InDiskCacheType._32K;
                case 64 * 1024:
                    return InDiskCacheType._64K;
                case 128 * 1024:
                    return InDiskCacheType._128K;
                case 256 * 1024:
                    return InDiskCacheType._256K;
                case 512 * 1024:
                    return InDiskCacheType._512K;
                case 1024 * 1024:
                    return InDiskCacheType._1M;
                case 2 * 1024 * 1024:
                    return InDiskCacheType._2M;
                case 4 * 1024 * 1024:
                    return InDiskCacheType._4M;
                case 8 * 1024 * 1024:
                    return InDiskCacheType._8M;
                case 16 * 1024 * 1024:
                    return InDiskCacheType._16M;
                case 32 * 1024 * 1024:
                    return InDiskCacheType._32M;
                default:
                    return InDiskCacheType._32M;
            }
        }

        private IInDiskCacheManager CreateInDiskCache(InDiskCacheType type)
        {
            return new SingleInDiskCacheManager(type, this.CacheDirectory);
        }        
    }
}
