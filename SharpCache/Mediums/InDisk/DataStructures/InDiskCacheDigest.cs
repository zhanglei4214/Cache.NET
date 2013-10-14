﻿namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using SharpCache.Common;
    using SharpCache.Mediums.InDisk.Services;
    #endregion

    internal class InDiskCacheDigest : ISerializable
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

        public static InDiskCacheDigest Deserialize(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public bool Build(PathSector sector)
        {
            this.Stream = this.allocator.CreateCacheFile(sector);

            return this.Stream == null ? false : true;
        }

        public void Remove(long index)
        {
            this.indexMap.Remove(index);
        }

        public bool TrySet(long index, out InDiskCacheItemMetaData meta)
        {
            return this.indexMap.TrySet(index, out meta);
        }

        public bool TryGet(long index, out InDiskCacheItemMetaData meta)
        {
            return this.indexMap.TryGet(index, out meta);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
