namespace SharpCache.Schedulers
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SharpCache.EventArguments;
    using SharpCache.Interfaces;
    using Microsoft.Practices.Prism.Logging;
    #endregion

    internal abstract class SchedulerBase : ICacheScheduler, IDisposable
    {
        #region Fileds

        protected List<ICacheMedium> mediums;

        protected readonly ILoggerFacade logger;

        #endregion

        #region Constructors

        public SchedulerBase(SchedulerConfiguration configuration, ILoggerFacade logger)
        {
            this.mediums = new List<ICacheMedium>();

            this.logger = logger;

            this.CreateCacheHierarchy(configuration.MediumSizeList);

            this.UpdateReplacementAlgorithms(configuration);

            this.HookEvents();
        }

        #endregion

        #region Properties

        public static SchedulerFactory Factory
        {
            get
            {
                return SchedulerFactory.Instance;
            }
        }

        #endregion

        #region Public Methods

        public int CacheLevel()
        {
            return this.mediums.Count;
        }

        public CacheValue Get(CacheKey key)
        {
            if (this.logger != null)
            {
                this.logger.Log("Get " + key.ToString(), Category.Info, Priority.Medium);
            }

            foreach (ICacheMedium medium in this.mediums)
            {
                CacheValue value = medium.Get(key);
                if (value != null)
                {
                    return value;
                }
            }

            if (this.logger != null)
            {
                this.logger.Log("Miss " + key.ToString(), Category.Info, Priority.Medium);
            }

            return null;
        }

        public bool Set(CacheKey key, CacheValue value)
        {
            if (this.logger != null)
            {
                this.logger.Log("Set " + key.ToString() + ":" + value.ToString(), Category.Info, Priority.Medium);
            }

            if (this.Remove(key) == false)
            {
                return false;
            }

            return this.mediums[0].Set(key, value);
        }

        public bool Remove(CacheKey key)
        {
            if (this.logger != null)
            {
                this.logger.Log("Remove " + key.ToString(), Category.Info, Priority.Medium);
            }

            foreach (ICacheMedium medium in this.mediums)
            {
                medium.Remove(key);
            }

            return true;
        }

        public bool Exists(CacheKey key)
        {
            return this.Get(key) == null ? false : true;
        }

        public void Clear()
        {
            if (this.logger != null)
            {
                this.logger.Log("Clear the entire cache.", Category.Info, Priority.Medium);
            }

            foreach (ICacheMedium medium in this.mediums)
            {
                medium.Clear();
            }
        }

        public void Adjust(SchedulerConfiguration configuration)
        {
            if (this.logger != null)
            {
                this.logger.Log("Adjust scheduler configuration.", Category.Info, Priority.High);
            }

            this.UpdateReplacementAlgorithms(configuration);
        }

        public string Dump()
        {
            StringBuilder sb = new StringBuilder();

            foreach (ICacheMedium medium in this.mediums)
            {
                sb.Append(medium.Dump());
            }

            return sb.ToString();
        }

        public void Dispose()
        {
            this.UnHookEvents();

            this.mediums.Clear();
            this.mediums = null;
        }

        #endregion

        #region Protected Methods

        protected abstract void CreateCacheHierarchy(CacheCapacity[] cacheCapacityList);

        #endregion

        #region Private Methods

        private void HookEvents()
        {
            foreach (ICacheMedium medium in this.mediums)
            {
                medium.GetCacheItemEvent    += new EventHandler<GetCacheItemEventArgs>(this.GetCacheItemEventHandler);
                medium.AddCacheItemEvent    += new EventHandler<AddCacheItemEventArgs>(this.AddCacheItemEventHandler);
                medium.RemoveCacheItemEvent += new EventHandler<RemoveCacheItemEventArgs>(this.RemoveCacheItemEventHandler);
                medium.ClearCacheItemsEvent += new EventHandler(this.ClearCacheItemsEventHander);
            }
        }

        private void UnHookEvents()
        {
            foreach (ICacheMedium medium in this.mediums)
            {
                medium.GetCacheItemEvent    -= new EventHandler<GetCacheItemEventArgs>(this.GetCacheItemEventHandler);
                medium.AddCacheItemEvent    -= new EventHandler<AddCacheItemEventArgs>(this.AddCacheItemEventHandler);
                medium.RemoveCacheItemEvent -= new EventHandler<RemoveCacheItemEventArgs>(this.RemoveCacheItemEventHandler);
                medium.ClearCacheItemsEvent -= new EventHandler(this.ClearCacheItemsEventHander);
            }
        }

        private void UpdateReplacementAlgorithms(SchedulerConfiguration configuration)
        {
            this.ValidateReplacementAlgorithm(configuration.Algorithms);

            for(int i = 0; i < this.mediums.Count; ++i)
            {
                this.mediums[i].ReplacementAlgorithm = configuration.Algorithms[i];
            }
        }

        private void ValidateReplacementAlgorithm(IReplacementAlgorithm[] algorithms)
        {
            if (algorithms == null)
            {
                throw new NullReferenceException("algorithm is NULL.");
            }

            if (this.mediums.Count != algorithms.Length)
            {
                throw new InvalidOperationException("algorithm number is not indentical to medium number.");
            }
        }

        private void AddCacheItemEventHandler(object sender, AddCacheItemEventArgs e)
        {
            if (this.logger != null)
            {
                this.logger.Log("AddCacheItemEventHander is invoked.", Category.Debug, Priority.Low);
            }

            ICacheMedium medium = sender as ICacheMedium;

            if (medium == null)
            {
                return;
            }

            IReplacementAlgorithm algorithm = medium.ReplacementAlgorithm;
            if (algorithm == null)
            {
                return;
            }

            algorithm.Add(e.Items.Select<CacheItem, CacheSummary>(item => item.GetSummary()).ToArray<CacheSummary>(), medium as IReplaceableCache);

            this.ReplaceItemsIfNecessary(medium);
        }

        private void ReplaceItemsIfNecessary(ICacheMedium medium)
        {
            CacheSummary[] items = medium.Replace();

            if (items == null)
            {
                return;
            }

            ICacheMedium next = medium.Next();

            if (next != null)
            {
                foreach (CacheSummary item in items)
                {
                    if (item.MetaData.IsExpired() == false)
                    {
                        // set the cache item to next medium
                        next.Set(item.Key, medium.Get(item.Key));
                    }
                }
            }

            foreach (CacheSummary item in items)
            {
                // remove the cache item from previous medium
                medium.Remove(item.Key);
            }
        }

        private void GetCacheItemEventHandler(object sender, GetCacheItemEventArgs e)
        {
            if (this.logger != null)
            {
                this.logger.Log("GetCacheItemEventHandler is invoked.", Category.Debug, Priority.Low);
            }

            ICacheMedium medium = sender as ICacheMedium;

            if (medium == null)
            {
                return;
            }

            IReplacementAlgorithm algorithm = medium.ReplacementAlgorithm;
            if (algorithm == null)
            {
                return;
            }

            algorithm.Get(e.Key, medium as IReplaceableCache);

            if (medium == this.mediums[0])
            {
                return;
            }            

            this.MoveCacheItem(medium, this.mediums[0], e.Key, e.Value);
        }

        private void MoveCacheItem(ICacheMedium from, ICacheMedium to, CacheKey key, CacheValue value)
        {
            if (this.logger != null)
            {
                this.logger.Log("Move cache item: " + key.ToString() + " from " + from.CacheName + " to " + to.CacheName, Category.Info, Priority.Medium);
            }

            from.Remove(key);

            to.Set(key, value);
        }

        private void RemoveCacheItemEventHandler(object sender, RemoveCacheItemEventArgs e)
        {
            ICacheMedium medium = sender as ICacheMedium;

            if (medium == null)
            {
                return;
            }

            IReplacementAlgorithm algorithm = medium.ReplacementAlgorithm;
            if (algorithm == null)
            {
                return;
            }

            algorithm.Remove(e.Key, medium as IReplaceableCache);
        }

        private void ClearCacheItemsEventHander(object sender, EventArgs e)
        {
        }

        #endregion
    }
}
