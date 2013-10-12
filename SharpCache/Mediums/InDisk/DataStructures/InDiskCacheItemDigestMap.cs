using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpCache.Mediums.InDisk.DataStructures
{
    internal class InDiskCacheItemDigestMap
    {
        #region Constructors

        public InDiskCacheItemDigestMap()
        {
        }

        #endregion

        #region Public Methods

        public bool TryGet(long index, out InDiskCacheItemDigest info)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
