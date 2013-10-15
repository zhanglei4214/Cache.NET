namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using SharpCache.Common;
    using SharpCache.Mediums.InDisk.Services;
    using SharpCache.Interfaces;
    #endregion

    internal class InDiskCacheDigest 
    {
        #region Fields

        private readonly FileAllocator allocator;

        private readonly InDiskCacheItemMetaDataMap indexMap;
        
        private FileStream stream;

        #endregion

        #region Constructors

        public InDiskCacheDigest(FileAllocator allocator)
        {
            this.allocator = allocator;

            this.indexMap = new InDiskCacheItemMetaDataMap();
        }

        #endregion

        #region Properties

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

        public void Remove(long index)
        {
            this.indexMap.Remove(index);
        }

        public bool Set(IHashable index, CacheItemMetaData meta, byte[] value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
