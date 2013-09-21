namespace NCache.Schedulers
{
    #region Using Directives
    using System;
    using NCache.Mediums;
    #endregion

    internal class FileScheduler : SchedulerBase
    {
        #region Fields

        #endregion

        #region Constructors

        public FileScheduler(SchedulerConfiguration configuration)
            : base(configuration)
        {
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
        }

        #endregion
    }
}
