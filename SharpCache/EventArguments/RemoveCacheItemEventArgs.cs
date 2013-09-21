namespace SharpCache.EventArguments
{
    #region Using Directives
    using System;
    #endregion

    internal class RemoveCacheItemEventArgs : EventArgs
    {
        #region Fields

        private readonly CacheKey key;

        #endregion

        #region Constructors

        public RemoveCacheItemEventArgs(CacheKey key)
            : base()
        {
            this.key = key;
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

        #endregion
    }
}
