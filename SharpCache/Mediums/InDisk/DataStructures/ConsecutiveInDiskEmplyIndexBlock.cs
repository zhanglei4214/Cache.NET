namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    #endregion

    internal class ConsecutiveInDiskEmplyIndexBlock
    {
        #region Fields

        private readonly int count;

        private readonly int capacity;

        #endregion

        #region Constructors

        public ConsecutiveInDiskEmplyIndexBlock(int count, int capacity)
        {
            this.count = count;

            this.capacity = capacity;

            this.First = new InDiskIndex(this.capacity);            
        }

        #endregion

        #region Properties

        public InDiskIndex First { get; set; }

        public InDiskIndex Last { get; set; }

        public long Remaining { get; private set; }

        public long Count
        {
            get
            {
                return this.count;
            }
        }

        #endregion
    }
}
