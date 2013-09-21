namespace SharpCache
{
    #region Using Directives
    using SharpCache.Interfaces;
    using SharpCache.Schedulers;
    #endregion

    internal class CacheProxy : CacheBase
    {
        #region Fields

        private ICacheScheduler scheduler;

        #endregion

        #region Constructors

        public CacheProxy(string name, CacheConfiguration configuration)
            : base(name)
        {
            this.ConfigureHierarchy(configuration);
        }

        #endregion

        #region Public Methods

        public override string Dump()
        {
            return this.scheduler.Dump();
        }

        #endregion

        #region Protected Methods

        protected override CacheValue DoGet(CacheKey key)
        {
            return this.scheduler.Get(key);
        }

        protected override bool DoSet(CacheKey key, CacheValue value)
        {
            return this.scheduler.Set(key, value);
        }

        protected override bool DoExists(CacheKey key)
        {
            return this.scheduler.Exists(key);
        }

        protected override void DoClear()
        {
            this.scheduler.Clear();
        }

        protected override void DoAdjust(CacheConfiguration configuration)
        {
            this.scheduler.Adjust(configuration.SchedulerConfiguration);
        }

        #endregion

        #region Private Methods

        private void ConfigureHierarchy(CacheConfiguration configuration)
        {
            this.scheduler = SchedulerBase.Factory.Create(configuration.SchedulerConfiguration);
        }

        #endregion
    }
}
