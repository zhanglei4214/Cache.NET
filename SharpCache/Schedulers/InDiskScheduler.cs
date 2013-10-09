namespace SharpCache.Schedulers
{
    #region Using Directives
    using System;
    using Microsoft.Practices.Prism.Logging;
    using SharpCache.Mediums;
    #endregion

    internal class InDiskScheduler : SchedulerBase
    {
        #region Fields

        #endregion

        #region Constructors

        public InDiskScheduler(SchedulerConfiguration configuration, ILoggerFacade logger)
            : base(configuration, logger)
        {
            if (this.logger != null)
            {
                this.logger.Log("InDiskScheduler is created.", Category.Info, Priority.High);
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

            InDiskCache inDiskCache = new InDiskCache(CacheMediumType.InDisk.ToString(), cacheCapacityList[0], this.logger);

            inDiskCache.PreviousCacheMedium = null;
            inDiskCache.NextCacheMedium = null;

            this.mediums.Add(inDiskCache);

            if (this.logger != null)
            {
                this.logger.Log("First medium is InDisk.", Category.Debug, Priority.Low);
            }
        }

        #endregion
    }
}
