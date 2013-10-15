namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    using System.Collections.Generic;
    using SharpCache.Interfaces;
    #endregion

    internal class InDiskCacheDigestSelector 
    {
        #region Fields

        private readonly Dictionary<IHashable, InDiskCacheType> router;

        private readonly Dictionary<InDiskCacheType, InDiskCacheDigest> digests;

        #endregion

        #region Constructors

        public InDiskCacheDigestSelector()
        {
            this.router = new Dictionary<IHashable,InDiskCacheType>();

            this.digests = new Dictionary<InDiskCacheType, InDiskCacheDigest>();
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public bool Remove(IHashable index)
        {
            return this.router.Remove(index);
        }

        public InDiskCacheDigest Get(IHashable index)
        {
            InDiskCacheType type;
            if (this.router.TryGetValue(index, out type) == true)
            {
                return this.digests[type];
            }

            return null;
        }

        public InDiskCacheDigest Insert(IHashable index, long length)
        {
            InDiskCacheDigest digest;
            InDiskCacheType type = this.CalculateCacheType(length);
            if (this.digests.ContainsKey(type) == false)
            {
                digest = this.CreateInDiskCache(type);

                this.digests[type] = digest;
            }
            else
            {
                digest = this.digests[type];
            }

            this.router[index] = type;

            return digest;
        }

        #endregion

        private InDiskCacheType CalculateCacheType(long length)
        {
            //// TODO: calculate the cache type.
            throw new System.NotImplementedException();
        }

        private InDiskCacheDigest CreateInDiskCache(InDiskCacheType type)
        {
            //// TODO: create in disk cache based on the type;
            throw new System.NotImplementedException();
        }
    }
}
