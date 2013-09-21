namespace SharpCache
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using SharpCache.Interfaces;
    using SharpCache.Schedulers;
    #endregion

    public class CacheManager : ICacheManager
    {
        #region Fields

        private List<ICache> caches;

        #endregion

        #region Constructors

        public CacheManager()
        {
            this.caches = new List<ICache>();
        }

        #endregion

        #region Properties

        public ICache[] Caches
        {
            get
            {
                return this.caches.ToArray();
            }
        }

        public ICache this[string name]
        {
            get
            {
                return this.Contains(name);
            }
        }

        #endregion

        #region Public Methods

        public ICache Contains(string name)
        {
            if (string.IsNullOrEmpty(name) == true)
            {
                throw new NullReferenceException("name is null.");
            }

            return this.caches.Find(cache => cache.Name == name);
        }

        public ICache Create(string name)
        {
            if (this.Contains(name) != null)
            {
                return null;
            }

            CacheConfiguration configuration = new CacheConfiguration(SchedulerType.RAMScheduler);

            ICache cache = new Cache(name, configuration);

            this.caches.Add(cache);

            return cache;
        }

        public ICache Create(string name, CacheConfiguration configuration)
        {
            if (this.Contains(name) != null)
            {
                return null;
            }

            if (configuration == null)
            {
                throw new NullReferenceException("configuration is NULL.");
            }

            ICache cache = new Cache(name, configuration);

            this.caches.Add(cache);

            return cache;
        }

        public bool Remove(string name)
        {
            ICache cache = this.Contains(name);
            if (cache == null)
            {
                return false;
            }

            return this.caches.Remove(cache);
        }

        #endregion
    }
}
