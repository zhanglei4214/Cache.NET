namespace SharpCache
{
    #region Using Directives
    #endregion

    public class CacheSummary
    {
        #region Fields

        private CacheKey key;

        private CacheItemMetaData metaData;

        #endregion

        #region Constructors

        public CacheSummary(CacheKey key, CacheItemMetaData metaData)
        {
            this.key = key;
            this.metaData = metaData;
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

        public CacheItemMetaData MetaData
        {
            get
            {
                return this.metaData;
            }

            set
            {
                this.metaData = value;
            }
        }

        #endregion
    }
}
