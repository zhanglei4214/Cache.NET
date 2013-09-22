namespace SharpCache.Schedulers
{
    #region Using Directives
    using System;
    using Microsoft.Practices.Prism.Logging;
    using SharpCache.Mediums;
    #endregion

    internal class FileScheduler : SchedulerBase
    {
        #region Fields

        #endregion

        #region Constructors

        public FileScheduler(SchedulerConfiguration configuration, ILoggerFacade logger)
            : base(configuration, logger)
        {
            if (this.logger != null)
            {
                this.logger.Log("FileScheduler is created.", Category.Info, Priority.High);
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

            FileCache file = new FileCache(CacheMediumType.File.ToString(), cacheCapacityList[0]);

            file.PreviousCacheMedium = null;
            file.NextCacheMedium = null;

            this.mediums.Add(file);

            if (this.logger != null)
            {
                this.logger.Log("First medium is File.", Category.Debug, Priority.Low);
            }
        }

        #endregion
    }
}
