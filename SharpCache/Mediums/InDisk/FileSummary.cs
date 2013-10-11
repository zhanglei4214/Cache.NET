namespace SharpCache.Mediums.InDisk
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using SharpCache.Common;
    #endregion

    internal class FileSummary : ISerializable
    {
        #region Fields

        private Dictionary<long, FileIndex> indexDict;

        private FileStream stream;

        #endregion

        #region Constructors

        public FileSummary()
        {
            this.indexDict = new Dictionary<long, FileIndex>();
        }

        #endregion

        #region Properties

        public FileIndex this[CacheKey key]
        {
            get
            {
                Ensure.ArgumentNotNull(key, "key");

                if (this.indexDict.ContainsKey(key.InternalIndex) == true)
                {
                    return this.indexDict[key.InternalIndex];
                }

                return null;
            }
        }

        public FileStream Stream
        {
            get
            {
                return this.stream;
            }

            set
            {
                this.stream = value;
            }
        }

        #endregion

        #region Public Methods

        public static FileSummary Deserialize(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
