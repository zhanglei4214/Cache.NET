namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    #endregion

    internal class InDiskCacheItemDigest
    {
        #region Constructors

        public InDiskCacheItemDigest()
        {
        }

        #endregion

        #region Properties

        public int Offset { get; set; }

        public int Length { get; set; }

        #endregion
    }
}
