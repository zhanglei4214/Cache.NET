namespace NCache
{
    #region Using Directives
    using System;
    #endregion

    public class CacheValue
    {
        #region Fields

        private CacheItemMetaData metaData;

        private object content;

        #endregion

        #region Constructors

        public CacheValue()
        {
            this.metaData = new CacheItemMetaData(CachePriority.P3);
        }

        #endregion

        #region Properties

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

        public object Content
        {
            get
            {
                return this.content;
            }

            set
            {
                this.content = value;
            }
        }

        #endregion

        #region Public Methods

        public static implicit operator CacheValue(string content)
        {
            CacheValue value = new CacheValue();
            value.Content = content;

            return value;
        }

        public static implicit operator CacheValue(string[] content)
        {
            CacheValue value = new CacheValue();
            value.Content = content;

            return value;
        }

        public static implicit operator CacheValue(int content)
        {
            CacheValue value = new CacheValue();
            value.Content = content;

            return value;
        }

        public static implicit operator CacheValue(long content)
        {
            CacheValue value = new CacheValue();
            value.Content = content;

            return value;
        }

        public static implicit operator CacheValue(short content)
        {
            CacheValue value = new CacheValue();
            value.Content = content;

            return value;
        }

        public static implicit operator CacheValue(bool content)
        {
            CacheValue value = new CacheValue();
            value.Content = content;

            return value;
        }

        public static implicit operator CacheValue(double content)
        {
            CacheValue value = new CacheValue();
            value.Content = content;

            return value;
        }

        #endregion
    }
}
