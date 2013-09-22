namespace SharpCache.Schedulers
{
    #region Using Directives
    using System;
    using Microsoft.Practices.Prism.Logging;
    using SharpCache.Mediums;
    #endregion

    internal class RAMScheduler : SchedulerBase, IDisposable
    {
        #region Fields

        #endregion

        #region Constructors

        public RAMScheduler(SchedulerConfiguration configuration, ILoggerFacade logger)
            : base(configuration, logger)
        {
            if (this.logger != null)
            {
                this.logger.Log("RAMScheduler is created.", Category.Info, Priority.High);
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

            RAMCache ram = new RAMCache(CacheMediumType.RAM.ToString(), cacheCapacityList[0], this.logger);

            ram.PreviousCacheMedium = null;
            ram.NextCacheMedium = null;

            this.mediums.Add(ram);

            if (this.logger != null)
            {
                this.logger.Log("First medium is RAM.", Category.Debug, Priority.Low);
            }
        }

        #endregion
    }
}
