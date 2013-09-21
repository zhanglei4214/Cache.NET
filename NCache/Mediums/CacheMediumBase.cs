namespace NCache.Mediums
{
    #region Using Directives
    using System;
    using System.Text;
    using NCache.Algorithms;
    using NCache.EventArguments;
    using NCache.Interfaces;
    #endregion

    internal abstract class CacheMediumBase : ICacheMedium, IReplaceableCache
    {
        #region Fields

        private CacheCapacity capacity;

        private readonly string name;

        private ICacheMedium next;

        private ICacheMedium previous;

        private IReplacementAlgorithm algorithm;

        private readonly AlgorithmData algorithmData;

        private object rwLock;

        #endregion

        #region Constructors

        public CacheMediumBase(string name, CacheCapacity capacity)
        {
            this.Capacity = capacity;

            this.name = name;
            this.algorithm = null;

            this.next = null;
            this.previous = null;

            this.algorithmData = new AlgorithmData();

            this.rwLock = new object();
        }

        #endregion

        #region Events

        public event EventHandler<GetCacheItemEventArgs> GetCacheItemEvent;

        public event EventHandler<AddCacheItemEventArgs> AddCacheItemEvent;

        public event EventHandler<RemoveCacheItemEventArgs> RemoveCacheItemEvent;

        public event EventHandler ClearCacheItemsEvent;

        #endregion

        #region Properties

        public string CacheName
        {
            get
            {
                return this.name;
            }
        }

        public CacheMediumType Type
        {
            get
            {
                return this.CacheMediumType();
            }
        }

        public IReplacementAlgorithm ReplacementAlgorithm
        {
            get
            {
                return this.algorithm;
            }
            set
            {
                this.algorithm = value;
            }
        }

        public ICacheMedium NextCacheMedium
        {
            set
            {
                this.next = value;
            }
        }

        public ICacheMedium PreviousCacheMedium
        {
            set
            {
                this.previous = value;
            }
        }


        public CacheCapacity Capacity
        {
            get
            {
                return this.capacity;
            }
            set
            {
                this.capacity = value;
            }
        }

        public AlgorithmData AlgorithmData
        {
            get
            {
                return this.algorithmData;
            }
        }
        #endregion

        #region Public Methods

        public CacheValue Get(CacheKey key)
        {
            lock (this.rwLock)
            {
                CacheValue value = this.DoGet(key);

                if (value != null)
                {
                    value.MetaData.Hittimes++;
                    value.MetaData.LastUpdated = DateTime.Now.Ticks;
                }

                if (this.GetCacheItemEvent != null && value != null)
                {
                    this.GetCacheItemEvent.Invoke(this, new GetCacheItemEventArgs(key, value));
                }

                return value;
            }
        }

        public bool Set(CacheKey key, CacheValue value)
        {
            return this.Set(new CacheItem[] { new CacheItem(key, value) });
        }

        public bool Set(CacheItem[] items)
        {
            lock (this.rwLock)
            {
                bool result = this.DoSet(items);

                if (result == true)
                {
                    foreach (CacheItem item in items)
                    {
                        item.Value.MetaData.Hittimes++;
                        item.Value.MetaData.LastUpdated = DateTime.Now.Ticks;
                    }
                }

                if (this.AddCacheItemEvent != null && result == true)
                {
                    this.AddCacheItemEvent.Invoke(this, new AddCacheItemEventArgs(items));
                }

                return result;
            }
        }

        public bool Remove(CacheKey key)
        {
            lock (this.rwLock)
            {
                bool result = this.DoRemove(key);

                if (this.RemoveCacheItemEvent != null)
                {
                    this.RemoveCacheItemEvent.Invoke(this, new RemoveCacheItemEventArgs(key));
                }

                return result;
            }
        }

        public void Clear()
        {
            lock (this.rwLock)
            {
                this.DoClear();

                if (this.ClearCacheItemsEvent != null)
                {
                    this.ClearCacheItemsEvent.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public string Dump()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Cache Name: " + this.name + "    Cache Type: " + this.Type.ToString());
            sb.AppendLine("Cache Capacity: " + this.capacity.ToString());
            sb.AppendLine("Replacement Algorithm: " + this.algorithm.ToString());

            sb.AppendLine(this.DoDump());

            return sb.ToString();
        }

        public ICacheMedium Next()
        {
            return this.next;
        }

        public ICacheMedium Previous()
        {
            return this.previous;
        }

        public CacheKey[] Replace()
        {
            if (this.algorithm == null)
            {
                return null;
            }

            return this.algorithm.Replace(this);
        }

        public long Count()
        {
            return this.DoCount();
        }

        public long MaxCount()
        {
            return this.DoMaxCount();
        }

        public long CacheSize()
        {
            return this.DoGetCacheSize();
        }

        #endregion

        #region Protected Methods

        protected abstract CacheMediumType CacheMediumType();

        protected abstract CacheValue DoGet(CacheKey key);

        protected abstract bool DoSet(CacheItem[] items);

        protected abstract bool DoRemove(CacheKey key);

        protected abstract void DoClear();

        protected abstract string DoDump();

        protected abstract long DoCount();

        protected abstract long DoMaxCount();

        protected abstract long DoGetCacheSize();

        #endregion
    }
}
