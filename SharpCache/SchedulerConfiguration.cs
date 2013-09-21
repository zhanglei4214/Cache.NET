namespace SharpCache
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using SharpCache.Algorithms;
    using SharpCache.Interfaces;
    using SharpCache.Schedulers;
    #endregion

    public class SchedulerConfiguration
    {
        #region Fields

        private SchedulerType type;
        /// <summary>
        /// Medium Size List
        /// </summary>
        private List<CacheCapacity> mediumSizeList;

        private List<IReplacementAlgorithm> algorithms;

        #endregion

        #region Constructors

        public SchedulerConfiguration(SchedulerType type)
        {
            switch (type)
            {
                case SchedulerType.FileScheduler:
                    this.DefaultFileSchedulerConfiguration();
                    break;
                case SchedulerType.RAMScheduler:
                    this.DefaultRAMSchedulerConfiguration();
                    break;
                case SchedulerType.RAMFileScheduler:
                    this.DefaultRAMFileSchedulerConfiguration();
                    break;
                default:
                    throw new NotSupportedException("scheduler type is not supported.");
            }
        }

        #endregion

        #region Properties

        public CacheCapacity[] MediumSizeList
        {
            get
            {
                return this.mediumSizeList.ToArray();
            }
        }

        public SchedulerType Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
            }
        }

        public IReplacementAlgorithm[] Algorithms
        {
            get
            {
                return this.algorithms.ToArray();
            }
        }

        #endregion

        #region Public Methods

        public void ChangeCacheCapacity(int index, CacheCapacity capacity)
        {
            if (capacity == null)
            {
                throw new NullReferenceException("capacity is null.");
            }

            int mediumSize = this.algorithms.Capacity;

            if (index < 0 || index >= mediumSize)
            {
                throw new IndexOutOfRangeException("index is out of range.");
            }

            this.mediumSizeList[index] = capacity;
        }

        public void ChangeAlgorithm(int index, IReplacementAlgorithm algorithm)
        {
            if (algorithm == null)
            {
                throw new NullReferenceException("algorithm is null.");
            }

            int mediumSize = this.algorithms.Capacity;

            if (index < 0 || index >= mediumSize)
            {
                throw new IndexOutOfRangeException("index is out of range.");
            }

            this.algorithms[index] = algorithm;
        }

        #endregion

        #region Private Methods

        private void DefaultRAMSchedulerConfiguration()
        {
            this.type = SchedulerType.RAMScheduler;

            this.algorithms = new List<IReplacementAlgorithm>(1);
            this.algorithms.Add(new LRUReplacementAlgorithm());

            this.mediumSizeList = new List<CacheCapacity>();
            this.mediumSizeList.Add(new CacheCapacity(Primary.COUNTONLY, 500));
        }

        private void DefaultFileSchedulerConfiguration()
        {
            this.type = SchedulerType.FileScheduler;

            this.algorithms = new List<IReplacementAlgorithm>(1);
            this.algorithms.Add(new LRUReplacementAlgorithm());

            this.mediumSizeList = new List<CacheCapacity>(1);
            this.mediumSizeList.Add(new CacheCapacity(Primary.COUNTONLY, 5000));
        }

        private void DefaultRAMFileSchedulerConfiguration()
        {
            this.type = SchedulerType.RAMFileScheduler;

            this.algorithms = new List<IReplacementAlgorithm>(2);
            //// Both RAM and file mediums adopt LRU replacement algorithm.
            this.algorithms.Add(new LRUReplacementAlgorithm());
            this.algorithms.Add(new LRUReplacementAlgorithm());

            this.mediumSizeList = new List<CacheCapacity>(2);
            this.mediumSizeList.Add(new CacheCapacity(Primary.COUNTONLY, 500));
            this.mediumSizeList.Add(new CacheCapacity(Primary.COUNTONLY, 5000));
        }

        #endregion
    }
}
