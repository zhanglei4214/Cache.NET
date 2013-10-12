namespace SharpCache.Mediums.InDisk.DataStructures
{
    #region Using Directives
    #endregion

    internal class PathSector
    {
        #region Fields

        private readonly int top;

        private readonly int second;

        private readonly int stub;

        #endregion

        #region Constructors

        public PathSector(int top, int second, int stub)
        {
            this.top = top;
            this.second = second;
            this.stub = stub;
        }

        #endregion

        #region Properties

        public int Top
        {
            get
            {
                return this.top;
            }
        }

        public int Second
        {
            get
            {
                return this.second;
            }
        }

        public int Stub
        {
            get
            {
                return this.stub;
            }
        }

        #endregion
    }
}
