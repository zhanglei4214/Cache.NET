namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    #endregion

    internal class InDiskCacheItemMetaData
    {
        #region Constructors

        public InDiskCacheItemMetaData()
        {
        }

        #endregion

        #region Properties

        public long Offset { get; set; }

        public long Length { get; set; }

        public long Capacity { get; set; }

        #endregion
    }
}
