namespace NCache.EventArguments
{
    #region Using Directives
    using System;
    #endregion

    internal class GetCacheItemEventArgs : EventArgs
    {
        #region Fields

        private readonly CacheKey key;

        private readonly CacheValue value;

        #endregion

        #region Constructors

        public GetCacheItemEventArgs(CacheKey key, CacheValue value)
            : base()
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
        }

        public CacheValue Value
        {
            get
            {
                return this.value;
            }
        }

        #endregion
    }
}
