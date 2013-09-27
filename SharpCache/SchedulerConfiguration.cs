namespace SharpCache
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SharpCache.Interfaces;
    using SharpCache.Mediums;
    using SharpCache.Schedulers;
    #endregion

    public class SchedulerConfiguration
    {
        #region Fields

        private SchedulerType type;

        private List<MediumConfiguration> mediumConfigurationList;

        #endregion

        #region Constructors

        public SchedulerConfiguration()
        {
            this.type = SchedulerType.Invalid;

            this.mediumConfigurationList = new List<MediumConfiguration>();
        }

        #endregion

        #region Properties

        public CacheCapacity[] MediumSizeList
        {
            get
            {
                return this.mediumConfigurationList.Select<MediumConfiguration, CacheCapacity>(c => c.Capacity).ToArray<CacheCapacity>();
            }
        }

        public SchedulerType Type
        {
            get
            {
                return this.type;
            }
        }

        public IReplacementAlgorithm[] Algorithms
        {
            get
            {
                return this.mediumConfigurationList.Select<MediumConfiguration, IReplacementAlgorithm>(c => c.Algorithm).ToArray<IReplacementAlgorithm>();
            }
        }

        #endregion

        #region Public Methods

        public void Add(MediumConfiguration item)
        {
            this.mediumConfigurationList.Add(item);

            this.AdjustScheduleType();
        }

        public void Clear()
        {
            this.mediumConfigurationList.Clear();

            this.AdjustScheduleType();
        }

        public void UpdateCacheCapacity(int index, CacheCapacity capacity)
        {
            if (capacity == null)
            {
                throw new NullReferenceException("capacity is null.");
            }

            if (index < 0 || index >= this.mediumConfigurationList.Count)
            {
                throw new IndexOutOfRangeException("index is out of range.");
            }

            this.mediumConfigurationList[index].UpdateCacheCapacity(capacity);
        }

        public void UpdateAlgorithm(int index, IReplacementAlgorithm algorithm)
        {
            if (algorithm == null)
            {
                throw new NullReferenceException("algorithm is null.");
            }

            if (index < 0 || index >= this.mediumConfigurationList.Count)
            {
                throw new IndexOutOfRangeException("index is out of range.");
            }

            this.mediumConfigurationList[index].UpdateAlgorithm(algorithm);
        }

        #endregion

        #region Private Methods

        private void AdjustScheduleType()
        {
            if (this.mediumConfigurationList.Count == 0)
            {
                this.type = SchedulerType.Invalid;
            }
            else if (this.mediumConfigurationList.Count == 1)
            {
                switch (this.mediumConfigurationList[0].Type)
                {
                    case CacheMediumType.File:
                        this.type = SchedulerType.FileScheduler;
                        break;
                    case CacheMediumType.RAM:
                        this.type = SchedulerType.RAMScheduler;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            else if (this.mediumConfigurationList.Count == 1)
            {
                this.type = SchedulerType.RAMFileScheduler;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        #endregion
    }
}
