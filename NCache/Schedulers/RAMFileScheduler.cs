namespace SharpCache.Schedulers
{
    #region Using Directives
    using System;
    using SharpCache.Mediums;
    #endregion

    internal class RAMFileScheduler : SchedulerBase
    {
        #region Constructors

        public RAMFileScheduler(SchedulerConfiguration configuration)
            : base(configuration)
        {
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
        }

        #endregion
    }
}
