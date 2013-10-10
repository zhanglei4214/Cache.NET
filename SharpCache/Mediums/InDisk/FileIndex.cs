namespace SharpCache.Mediums.InDisk
{
    #region Using Directives
    #endregion

    public class FileIndex
    {
        #region Constructors

        public FileIndex()
        {
        }

        #endregion

        #region Properties

        public int Offset { get; set; }

        public int Length { get; set; }

        #endregion
    }
}
