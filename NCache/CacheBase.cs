namespace NCache
{
    #region Using Directives
    using System;
    using NCache.Interfaces;
    #endregion

    internal abstract class CacheBase : ICache
    {
        #region Fields

        private readonly string name;

        #endregion

        #region Constructors

        public CacheBase(string name)
        {
            this.name = name;
        }

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public CacheValue this[CacheKey key]
        {
            get
            {
                return this.DoGet(key);
            }

            set
            {
                this.DoSet(key, value);
            }
        }

        #endregion

        #region Public Methods

        public bool Exists(CacheKey key)
        {
            bool result = this.DoExists(key);

            this.Signal();

            return result;
        }

        public void Clear()
        {
            this.DoClear();

            this.Signal();
        }

        public void Adjust(CacheConfiguration configuration)
        {
            this.DoAdjust(configuration);

            this.Signal();
        }

        public virtual string Dump()
        {
            throw new NotImplementedException("Derived class to implement");
        }

        #endregion

        #region Protected Methods

        protected abstract CacheValue DoGet(CacheKey key);

        protected abstract bool DoSet(CacheKey key, CacheValue value);

        protected abstract bool DoExists(CacheKey key);

        protected abstract void DoClear();

        protected abstract void DoAdjust(CacheConfiguration configuration);

        #endregion

        #region Private Methods

        private void Signal()
        {
        }

        #endregion
    }
}
