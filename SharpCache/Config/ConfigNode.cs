namespace SharpCache.Config
{
    #region Using Directives
    using SharpCache.Interfaces;
    using SharpCache.Mediums;
    using SharpCache.Schedulers;
    #endregion

    public class ConfigNode
    {
        #region Constructors

        public ConfigNode()
        {
            this.Level = -1;
            this.Type = CacheMediumType.None;
            this.Algorithm = null;
            this.Capacity = null;
        }

        #endregion

        #region Properties

        public int Level { get; set; }

        public CacheMediumType Type { get; set; }

        public IReplacementAlgorithm Algorithm { get; set; }

        public CacheCapacity Capacity { get; set; }

        #endregion

        #region Public Methods

        public bool Validate()
        {
            if (this.Level == -1)
            {
                return false;
            }

            if (this.Type == CacheMediumType.None)
            {
                return false;
            }

            if (this.Algorithm == null)
            {
                return false;
            }

            if (this.Capacity == null)
            {
                return false;
            }

            return true;
        }

        public MediumConfiguration Convert()
        {
            return new MediumConfiguration(this.Type, this.Capacity, this.Algorithm);
        }

        #endregion
    }
}
