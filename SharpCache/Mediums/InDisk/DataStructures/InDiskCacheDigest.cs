namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    using System;
    using System.IO;
    using SharpCache.Common;
    using SharpCache.Interfaces;
    #endregion

    internal class InDiskCacheDigest : IDisposable
    {
        #region Fields

        private readonly InDiskCacheItemMetaDataMap indexMap;

        private FileStream stream;

        private object fileLock = new object();

        #endregion

        #region Constructors

        public InDiskCacheDigest(InDiskCacheType type, string dir)
        {
            this.stream = this.CreateCacheFile(dir, type);

            this.indexMap = new InDiskCacheItemMetaDataMap();
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public void Remove(long index)
        {
            this.indexMap.Remove(index);
        }

        public bool Set(IHashable index, byte[] value)
        {
            Ensure.ArgumentNotNull(value, "value");

            InDiskCacheItemMetaData meta = this.indexMap.FindFree(index);

            return this.WriteToFile(meta.Offset, value, value.Length);
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
