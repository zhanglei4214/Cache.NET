namespace SharpCache.Schedulers
{
    #region Using Directives
    using System;
    using Microsoft.Practices.Prism.Logging;
    using SharpCache.Mediums;
    #endregion

    internal class MemoryDiskScheduler : SchedulerBase
    {
        #region Constructors

        public MemoryDiskScheduler(SchedulerConfiguration configuration, ILoggerFacade logger)
            : base(configuration, logger)
        {
            if (this.logger != null)
            {
                this.logger.Log("RAMFileScheduler is created.", Category.Info, Priority.High);
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Protected Methods

        protected override void CreateCacheHierarchy(CacheCapacity[] cacheCapacityList)
        {
            if (cacheCapacityList.Length != 2)
            {
                throw new Exception("CapacityList doesn't match SchedulerType");
            }

            InMemoryCache inMemoryCache = new InMemoryCache(CacheMediumType.InMemory.ToString(), cacheCapacityList[0], this.logger);

            InDiskCache file = new InDiskCache(CacheMediumType.InDisk.ToString(), cacheCapacityList[1], this.logger);

            inMemoryCache.PreviousCacheMedium = null;
            inMemoryCache.NextCacheMedium = file;

            file.PreviousCacheMedium = inMemoryCache;
            file.NextCacheMedium = null;

            this.mediums.Add(inMemoryCache);

            this.mediums.Add(file);

            if (this.logger != null)
            {
                this.logger.Log("First medium is InMemory, second medum is File.", Category.Debug, Priority.Low);
            }
        }

        #endregion
    }
}
