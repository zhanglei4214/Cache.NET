namespace SharpCache.Mediums
{
    #region Using Directives
    using System;
    using System.IO;
    using Microsoft.Practices.Prism.Logging;
    using SharpCache.Common;
    using SharpCache.Mediums.InDisk;
    #endregion

    internal class InDiskCache : CacheMediumBase
    {
        #region Fields

        private const int SMALL_FILE_SIZE = 2 * 1024 * 1024;

        private const int BIG_FILE_SIZE = 10 * SMALL_FILE_SIZE;

        private string cacheDir;

        private readonly ICacheFileManager fileManager;

        #endregion

        #region Constructors

        public InDiskCache(string name,CacheCapacity capacity, ILoggerFacade logger)
            : base(name, capacity, logger)
        {
            this.cacheDir = Environment.CurrentDirectory;

            this.fileManager = new CacheFileManager();
        }

        #endregion

        #region Properties

        public string CacheDir
        {
            get
            {
                return this.cacheDir;
            }

            set
            {
                this.cacheDir = value;
            }
        }

        #endregion

        #region Protected Methods

        protected override CacheMediumType CacheMediumType()
        {
            return Mediums.CacheMediumType.InDisk;
        }

        protected override CacheValue DoGet(CacheKey key)
        {
            string filePath = this.GetCacheFile(key);

            if (string.IsNullOrEmpty(filePath) == true)
            {
                return null;
            }

            return this.GetFromFile(filePath, key);
        }

        protected override bool DoSet(CacheItem[] items)
        {
            foreach (CacheItem item in items)
            {
                CacheFileSector sector = FileAllocator.Parse(item.Key);

                return this.fileManager.Set(sector, item.Value);
            }

            return true;
        }

        protected override bool DoRemove(CacheKey key)
        {
            return this.fileManager.Remove(key);
        }

        protected override void DoClear()
        {
            // TODO : Delete the files.
        }

        protected override string DoDump()
        {
            throw new NotImplementedException();
        }

        protected override long DoCount()
        {
            throw new NotImplementedException();
        }

        protected override long DoGetCacheSize()
        {
            throw new NotImplementedException();
        }

        protected override long DoMaxCount()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        private string GetCacheFile(CacheKey key)
        {
            return null;
        }

        private CacheValue GetFromFile(string path, CacheKey key)
        {
            CacheValue value = this.fileManager.FileIndex[path];
            FileSummary summary = null;

            if (value == null)
            {
                summary = this.GetFileSummary(path);

                this.fileManager.FileIndex[path] = new CacheValue { Content = summary };
            }

            summary.Stream.Seek(summary[key].Offset, SeekOrigin.Begin);

            byte[] valueInByte = new byte[summary[key].Length];
            summary.Stream.Read(valueInByte, 0, summary[key].Length);

            return CacheValue.Deserialize(valueInByte);
        }

        private FileSummary GetFileSummary(string path)
        {
            FileStream handler = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);

            Ensure.ArgumentNotNull(handler, "FileHandler");

            byte[] summaryLenInByte = new byte[10];
            handler.Seek(0, SeekOrigin.Begin);
            handler.Read(summaryLenInByte, 0, 10);

            int summaryLen = Toolkit.ConvertToIntegar(summaryLenInByte);

            byte[] summaryInstance = new byte[summaryLen];
            handler.Seek(10, SeekOrigin.Begin);
            handler.Read(summaryLenInByte, 0, summaryLen);

            FileSummary summary = FileSummary.Deserialize(summaryInstance);

            Ensure.ArgumentNotNull(summary, "Summary");

            summary.Stream = handler;

            return summary;
        }


        #endregion
    }
}
