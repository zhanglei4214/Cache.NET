namespace SharpCache
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    #endregion

    [Serializable]
    public class CacheItemMetaData
    {
        #region Fields

        /// <summary>
        /// priority of this block for replacement algorithm
        /// it should be generated dynamically by hit times,load time...
        /// </summary>
        private CachePriority priority;

        /// <summary>
        /// How many times this block is hitted
        /// </summary>
        private int hittimes;

        /// <summary>
        /// when this block is added into cache
        /// </summary>
        private long expire;

        /// <summary>
        /// time of least recent read or write
        /// </summary>
        private long lastUpdated;

        /// <summary>
        /// other discription info
        /// </summary>
        private Dictionary<string, string> discription;

        #endregion

        #region Constructors

        public CacheItemMetaData()
            : this(CachePriority.P3, long.MaxValue)
        {
        }

        public CacheItemMetaData(CachePriority priority)
            : this(priority, long.MaxValue)
        {
        }

        public CacheItemMetaData(long expire)
            : this(CachePriority.P3, expire)
        {
        }

        public CacheItemMetaData(CachePriority priority, long expire)
        {
            this.expire = expire;

            this.lastUpdated = DateTime.Now.Ticks;

            this.Hittimes = 0;

            this.priority = priority;

            this.discription = new Dictionary<string, string>();
        }

        #endregion

        #region Properties

        public long Expire
        {
            get
            {
                return this.expire;
            }
        }


        public long LastUpdated
        {
            get
            {
                return this.lastUpdated;
            }
            set
            {
                this.lastUpdated = value;
            }
        }


        public int Hittimes
        {
            get
            {
                return this.hittimes;
            }
            set
            {
                this.hittimes = value;
            }
        }


        public CachePriority Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                this.priority = value;
            }
        }

        public Dictionary<string, string> Discription
        {
            get
            {
                return this.discription;
            }
            set
            {
                this.discription = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves the current object  to a file
        /// </summary>
        /// <param name="path">The file path to save to</param>
        public void Save(String path)
        {
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, this);
            stream.Flush();
            stream.Close();
        }

        /// <summary>
        /// Instantiates object based on the serialization from the target file
        /// </summary>
        /// <param name="path">The path to the target load file</param>
        /// <returns> object based on the values found within the target file</returns>
        public static CacheItemMetaData Load(String path)
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            object o = bf.Deserialize(stream);
            stream.Flush();
            stream.Close();

            return o as CacheItemMetaData;
        }

        #endregion
    }
}