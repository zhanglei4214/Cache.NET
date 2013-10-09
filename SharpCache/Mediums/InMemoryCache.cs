﻿namespace SharpCache.Mediums
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.Prism.Logging;
    #endregion

    internal class InMemoryCache : CacheMediumBase
    {
        #region Fields

        private Dictionary<CacheKey, CacheValue> cacheDictionary;

        #endregion

        #region Constructors

        public InMemoryCache(string name, CacheCapacity capacity, ILoggerFacade logger)
            : base(name, capacity, logger)
        {
            // if there is no capacity parameter initialized, do not use cache
            if (this.Capacity.IsEmpty() == true)
            {
                throw new Exception("Cache capacity is not initialized.");
            }

            this.cacheDictionary = new Dictionary<CacheKey, CacheValue>();
        }

        #endregion

        #region Protected Methods

        protected override CacheMediumType CacheMediumType()
        {
            return Mediums.CacheMediumType.InMemory;
        }

        protected override CacheValue DoGet(CacheKey key)
        {
            if (this.cacheDictionary == null)
            {
                return null;
            }

            if (this.Exists(key) == true)
            {
                return this.cacheDictionary[key];
            }
            else
            {
                return null;
            }
        }

        protected override bool DoSet(CacheItem[] items)
        {
            if (this.cacheDictionary == null)
            {
                return false;
            }

            foreach (CacheItem item in items)
            {
                this.cacheDictionary[item.Key] = item.Value;
            }

            return true;
        }

        protected override bool DoRemove(CacheKey key)
        {
            if (this.cacheDictionary == null)
            {
                return false;
            }

            if (this.Exists(key) == true)
            {
                this.cacheDictionary.Remove(key);

                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void DoClear()
        {
            if (this.cacheDictionary != null)
            {
                this.cacheDictionary.Clear();
            }
        }

        protected override string DoDump()
        {
            StringBuilder sb = new StringBuilder();

            foreach (CacheKey key in this.cacheDictionary.Keys)
            {
                sb.AppendLine("key: " + key.InternalIndex + "    value: " + this.cacheDictionary[key].Content.ToString());
            }

            return sb.ToString();
        }

        // get cache dictionary existing item count
        protected override long DoCount()
        {
            if (this.cacheDictionary != null)
            {
                return this.cacheDictionary.Count;
            }
            else
            {
                return 0;
            }
        }

        // get maximum cache dictionary item count allowed
        protected override long DoMaxCount()
        {
            return this.Capacity.Count;
        }

        protected override long DoGetCacheSize()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        private bool Exists(CacheKey key)
        {
            if (this.cacheDictionary == null)
            {
                return false;
            }

            return this.cacheDictionary.ContainsKey(key);
        }

        #endregion
    }
}
