namespace NCache
{
    #region Using Directives
    using NCache.Schedulers;
    #endregion

    public class CacheConfiguration
    {
        #region Fields

        private SchedulerConfiguration schedulerConfiguration;

        #endregion

        #region Constructors

        public CacheConfiguration()
            : this(SchedulerType.RAMScheduler)
        {
        }

        public CacheConfiguration(SchedulerType type)
        {
            this.schedulerConfiguration = new SchedulerConfiguration(type);
        }

        #endregion

        #region Properties

        public SchedulerConfiguration SchedulerConfiguration
        {
            get
            {
                return this.schedulerConfiguration;
            }
        }

        #endregion
    }
}
