namespace SharpCache.Schedulers
{
    #region Using Directives
    using System;
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

        public ICacheScheduler Create(SchedulerConfiguration configuration)
        {
            switch (configuration.Type)
            {
                case SchedulerType.RAMScheduler:
                    return new RAMScheduler(configuration);
                case SchedulerType.FileScheduler:
                    return new FileScheduler(configuration);
                case SchedulerType.RAMFileScheduler:
                    return new RAMFileScheduler(configuration);
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
