namespace NCache
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using NCache.EventArguments;
    using NCache.Interfaces;
    #endregion

    public class CacheItem
    {
        #region Fields

        private CacheKey key;

        private CacheValue value;

        #endregion

        #region Constructors

        public CacheItem(CacheKey key, CacheValue value)
        {
            this.key = key;
            this.value = value;
        }

        #endregion

        #region Properties

        public CacheKey Key
        {
            get
            {
                return this.key;
            }

            set
            {
                this.key = value;
            }
        }

        public CacheValue Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        #endregion

        #region Public Methods

        public CacheSummary GetSummary()
        {
            return new CacheSummary(this.Key, this.Value.MetaData);
        }

        #endregion
    }
}
