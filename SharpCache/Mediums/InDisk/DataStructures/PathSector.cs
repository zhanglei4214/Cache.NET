namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    #endregion

    internal class PathSector
    {
        #region Fields

        private readonly long top;

        private readonly long second;

        private readonly long third;

        private readonly long stub;

        #endregion

        #region Constructors

        public PathSector(long top, long second, long stub)
        {
            this.top = top;
            this.second = second;
            this.stub = stub;
        }

        #endregion

        #region Properties

        public long Top
        {
            get
            {
                return this.top;
            }
        }

        public long Second
        {
            get
            {
                return this.second;
            }
        }

        public long Stub
        {
            get
            {
                return this.stub;
            }
        }

        #endregion
    }
}
