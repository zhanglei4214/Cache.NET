namespace NCache.EventArguments
{
    #region Using Directives
    using System;
    #endregion

    internal class AddCacheItemEventArgs : EventArgs
    {
        #region Fields

        private readonly CacheItem[] items;

        #endregion

        #region Constructors

        public AddCacheItemEventArgs(CacheItem[] items)
            : base()
        {
            this.items = items;
        }

        #endregion

        #region Properties

        public CacheItem[] Items
        {
            get
            {
                return this.items;
            }
        }

        #endregion
    }
}
