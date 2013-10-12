using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpCache.Mediums.InDisk.DataStructures
{
    internal class InDiskCacheItemMetaDataMap
    {
        #region Constructors

        public InDiskCacheItemMetaDataMap()
        {
        }

        #endregion

        #region Public Methods

        public bool TryGet(long index, out InDiskCacheItemMetaData info)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
