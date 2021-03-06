﻿namespace SharpCache
{
    #region Using Directives
    using Microsoft.Practices.Prism.Logging;
    using SharpCache.Schedulers;
    #endregion

    public class CacheConfiguration
    {
        #region Fields

        private SchedulerConfiguration schedulerConfiguration;

        #endregion

        #region Constructors

        public CacheConfiguration()
        {
            this.schedulerConfiguration = new SchedulerConfiguration();
        }

        #endregion

        #region Properties

        public ILoggerFacade Logger { get; set; }

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
