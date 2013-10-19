namespace SharpCache.Mediums.InDisk.DataStructures
{
    public class Block
    {
        #region Constructors

        public Block(int first, int last)
        {
            this.First = first;

            this.Last = last;
        }

        #endregion

        #region Properties

        public int First { get; set; }

        public int Last { get; set; }

        #endregion
    }
}
