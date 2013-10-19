namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    using System.IO;
    using SharpCache.Common;
    using SharpCache.Interfaces;
    using SharpCache.Mediums.InDisk.Interfaces;
    using SharpCache.Mediums.InDisk.Services;
    #endregion

    internal class SingleInDiskCacheManager : IInDiskCacheManager
    {
        #region Fields

        private readonly InDiskIndexManager indexManager;

        private readonly int itemSize;

        private FileStream stream;

        private object fileLock = new object();

        #endregion

        #region Constructors

        public SingleInDiskCacheManager(InDiskCacheType type, string dir)
        {
            this.itemSize = (int)type;

            this.stream = this.CreateCacheFile(dir, type);

            this.indexManager = new InDiskIndexManager();
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public bool Remove(IHashable key)
        {
            return this.indexManager.Remove(key);
        }

        public bool Set(IHashable key, CacheItemMetaData meta, byte[] value)
        {
            Ensure.ArgumentNotNull(value, "value");

            int index = this.indexManager.FindFree();

            return this.WriteToFile(index * this.itemSize, value, value.Length);
        }

        #endregion

        private FileStream CreateCacheFile(string dir, InDiskCacheType type)
        {
            lock (this.fileLock)
            {
                if (Directory.Exists(dir) == false)
                {
                    Directory.CreateDirectory(dir);
                }

                string path = Path.Combine(dir, type.ToString());

                return new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            }
        }

        private bool WriteToFile(long offset, byte[] value, int count)
        {
            try
            {
                this.stream.Seek(offset, SeekOrigin.Begin);

                this.stream.Write(value, 0, count);
            }
            catch
            {
                //// TODO: log the exception.
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            lock (this.fileLock)
            {
                if (this.stream != null)
                {
                    this.stream.Close();

                    this.stream = null;
                }
            }
        }
    }
}
