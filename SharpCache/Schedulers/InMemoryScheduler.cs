namespace SharpCache.Schedulers
{
    #region Using Directives
    using System;
    using Microsoft.Practices.Prism.Logging;
    using SharpCache.Mediums;
    #endregion

    internal class InMemoryScheduler : SchedulerBase, IDisposable
    {
        #region Fields

        #endregion

        #region Constructors

        public InMemoryScheduler(SchedulerConfiguration configuration, ILoggerFacade logger)
            : base(configuration, logger)
        {
            if (this.logger != null)
            {
                this.logger.Log("InMemoryScheduler is created.", Category.Info, Priority.High);
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Protected Methods

        protected override void CreateCacheHierarchy(CacheCapacity[] cacheCapacityList)
        {
            if (cacheCapacityList.Length != 1)
            {
                throw new Exception("CapacityList doesn't match SchedulerType");
            }

            InMemoryCache inMemoryCache = new InMemoryCache(CacheMediumType.InMemory.ToString(), cacheCapacityList[0], this.logger);

            inMemoryCache.PreviousCacheMedium = null;
            inMemoryCache.NextCacheMedium = null;

            this.mediums.Add(inMemoryCache);

            if (this.logger != null)
            {
                this.logger.Log("First medium is InMemory.", Category.Debug, Priority.Low);
            }
        }

        #endregion
    }
}
