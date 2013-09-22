namespace SharpCache.Schedulers
{
    #region Using Directives
    using System;
    using Microsoft.Practices.Prism.Logging;
    using SharpCache.Mediums;
    #endregion

    internal class RAMFileScheduler : SchedulerBase
    {
        #region Constructors

        public RAMFileScheduler(SchedulerConfiguration configuration, ILoggerFacade logger)
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

            RAMCache ram = new RAMCache(CacheMediumType.RAM.ToString(), cacheCapacityList[0]);

            FileCache file = new FileCache(CacheMediumType.File.ToString(), cacheCapacityList[1]);

            ram.PreviousCacheMedium = null;
            ram.NextCacheMedium = file;

            file.PreviousCacheMedium = ram;
            file.NextCacheMedium = null;

            this.mediums.Add(ram);

            this.mediums.Add(file);

            if (this.logger != null)
            {
                this.logger.Log("First medium is RAM, second medum is File.", Category.Debug, Priority.Low);
            }
        }

        #endregion
    }
}
