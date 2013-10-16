namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    #endregion

    internal class InDiskIndex
    {
        #region Fields

        private readonly int capacity;

        #endregion

        #region Constructors

        public InDiskIndex(int capacity)
        {
            this.capacity = capacity;
        }

        #endregion

        #region Properties

        public long Offset { get; set; }

        public int Length { get; set; }

        #endregion
    }
}
