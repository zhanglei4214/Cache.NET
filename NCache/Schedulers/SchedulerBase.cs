namespace SharpCache.Schedulers
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SharpCache.EventArguments;
    using SharpCache.Interfaces;
    #endregion

    internal abstract class SchedulerBase : ICacheScheduler, IDisposable
    {
        #region Fileds

        protected List<ICacheMedium> mediums;

        #endregion

        #region Constructors

        public SchedulerBase(SchedulerConfiguration configuration)
        {
            this.mediums = new List<ICacheMedium>();

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
            foreach (ICacheMedium medium in this.mediums)
            {
                CacheValue value = medium.Get(key);
                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }

        public bool Set(CacheKey key, CacheValue value)
        {
            if (this.Remove(key) == false)
            {
                return false;
            }

            return this.mediums[0].Set(key, value);
        }

        public bool Remove(CacheKey key)
        {
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
            foreach (ICacheMedium medium in this.mediums)
            {
                medium.Clear();
            }
        }

        public void Adjust(SchedulerConfiguration configuration)
        {
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
                medium.GetCacheItemEvent    += new EventHandler<GetCacheItemEventArgs>(this.GetCacheItemEventHander);
                medium.AddCacheItemEvent    += new EventHandler<AddCacheItemEventArgs>(this.AddCacheItemEventHander);
                medium.RemoveCacheItemEvent += new EventHandler<RemoveCacheItemEventArgs>(this.RemoveCacheItemEventHander);
                medium.ClearCacheItemsEvent += new EventHandler(this.ClearCacheItemsEventHander);
            }
        }

        private void UnHookEvents()
        {
            foreach (ICacheMedium medium in this.mediums)
            {
                medium.GetCacheItemEvent    -= new EventHandler<GetCacheItemEventArgs>(this.GetCacheItemEventHander);
                medium.AddCacheItemEvent    -= new EventHandler<AddCacheItemEventArgs>(this.AddCacheItemEventHander);
                medium.RemoveCacheItemEvent -= new EventHandler<RemoveCacheItemEventArgs>(this.RemoveCacheItemEventHander);
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

        private void AddCacheItemEventHander(object sender, AddCacheItemEventArgs e)
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

            algorithm.Add(e.Items.Select<CacheItem, CacheSummary>(item => item.GetSummary()).ToArray<CacheSummary>(), medium as IReplaceableCache);

            this.ReplaceItemsIfNecessary(medium);
        }

        private void ReplaceItemsIfNecessary(ICacheMedium medium)
        {
            CacheKey[] items = medium.Replace();

            if (items == null)
            {
                return;
            }

            ICacheMedium next = medium.Next();

            if (next != null)
            {
                foreach (CacheKey item in items)
                {
                    // set the cache item to next medium
                    next.Set(item, medium.Get(item));
                }
            }

            foreach (CacheKey item in items)
            {
                // remove the cache item from previous medium
                medium.Remove(item);
            }
        }

        private void GetCacheItemEventHander(object sender, GetCacheItemEventArgs e)
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

            algorithm.Get(e.Key, medium as IReplaceableCache);

            if (medium == this.mediums[0])
            {
                return;
            }

            this.MoveCacheItem(medium, this.mediums[0], e.Key, e.Value);
        }

        private void MoveCacheItem(ICacheMedium from, ICacheMedium to, CacheKey key, CacheValue value)
        {
            from.Remove(key);

            to.Set(key, value);
        }

        private void RemoveCacheItemEventHander(object sender, RemoveCacheItemEventArgs e)
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
