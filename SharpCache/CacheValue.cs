namespace SharpCache
{
    #region Using Directives
    using System;
    using System.Runtime.Serialization;
    using SharpCache.Common.Serialization;
    #endregion

    public class CacheValue : ISerializableCache
    {
        #region Fields

        private CacheItemMetaData metaData;

        private object content;

        #endregion

        #region Constructors

        public CacheValue()
        {
            this.metaData = new CacheItemMetaData();
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        public ISerializableCache Deserialize(byte[] value)
        {
            throw new NotImplementedException();
        }

        public bool IsExpired()
        {
            return this.metaData.IsExpired();
        }

        public override string ToString()
        {
            return "CacheValue: " + this.GetCacheValueSummary();
        }

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

        #region Private Methods

        private string GetCacheValueSummary()
        {
            string content = this.Content.ToString();

            if (content.Length < 30)
            {
                return content;
            }
            else
            {
                return content.Substring(0, 30) + "...";
            }
        }

        #endregion
    }
}
