namespace NCache
{
    #region Using Namespace
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using NCache.Hash;
    #endregion

    [Serializable]
    public class CacheKey
    {
        #region Fields

        private long hash;

        #endregion

        #region Constructors

        private CacheKey()
        {
        }

        private CacheKey(params string[] keys)
        {
            StringBuilder keyBuilder = new StringBuilder();

            foreach(string key in keys)
            {
                keyBuilder.Append(key);
            }

            this.hash = HashKeyGenerator.CalculateHash(keyBuilder.ToString());
        }

        private CacheKey(long hash)
        {
            this.hash = hash;
        }

        #endregion

        #region Properties

        public long InternalIndex
        {
            get
            {
                return this.hash;
            }
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            CacheKey cachekey = obj as CacheKey;

            if(cachekey == null)
            {
                return false;
            }

            return this.hash.Equals(cachekey.hash);
        }

        public override int GetHashCode()
        {
            return (int)this.InternalIndex;
        }

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
        public static CacheKey Load(String path)
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            object o = bf.Deserialize(stream);
            stream.Flush();
            stream.Close();

            return o as CacheKey;
        }

        #endregion

        #region static methods

        public static CacheKey Create(params string[] keys)
        {
            CacheKey cachekey = new CacheKey(keys);

            return cachekey;
        }

        public static CacheKey Create(long key)
        {
            CacheKey cachekey = new CacheKey(key);

            return cachekey;
        }

        public static implicit operator CacheKey(long key)
        {
            return Create(key);
        }

        public static implicit operator CacheKey(string key)
        {
            return Create(key);
        }

        #endregion

    }
}
