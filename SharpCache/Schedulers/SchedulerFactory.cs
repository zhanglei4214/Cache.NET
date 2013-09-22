namespace SharpCache.Schedulers
{
    #region Using Directives
    using System;
    using Microsoft.Practices.Prism.Logging;
    using SharpCache.Interfaces;
    #endregion

    internal sealed class SchedulerFactory
    {
        #region Constructors

        private SchedulerFactory()
        {
        }

        #endregion

        #region Properties

        public static SchedulerFactory Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        #endregion

        #region Public Methods

        public ICacheScheduler Create(SchedulerConfiguration configuration, ILoggerFacade logger)
        {
            switch (configuration.Type)
            {
                case SchedulerType.RAMScheduler:
                    return new RAMScheduler(configuration, logger);
                case SchedulerType.FileScheduler:
                    return new FileScheduler(configuration, logger);
                case SchedulerType.RAMFileScheduler:
                    return new RAMFileScheduler(configuration, logger);
                default:
                    throw new NotSupportedException("Not support scheduler");
            }
        }

        #endregion

        #region Nested class

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly SchedulerFactory instance = new SchedulerFactory();
        }

        #endregion
    }
}
