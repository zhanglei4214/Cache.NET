namespace NCache.Schedulers
{
    #region Using Directives
    using System;
    using NCache.Mediums;
    #endregion

    internal class RAMScheduler : SchedulerBase, IDisposable
    {
        #region Fields

        #endregion

        #region Constructors

        public RAMScheduler(SchedulerConfiguration configuration)
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

            RAMCache ram = new RAMCache(CacheMediumType.RAM.ToString(), cacheCapacityList[0]);

            ram.PreviousCacheMedium = null;
            ram.NextCacheMedium = null;

            this.mediums.Add(ram);
        }

        #endregion
    }
}
