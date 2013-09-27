namespace SharpCache.Mediums
{
    #region Using Directives
    using SharpCache.Interfaces;
    #endregion

    public class MediumConfiguration
    {
        #region Fields

        private readonly CacheMediumType type;

        private CacheCapacity capacity;

        private IReplacementAlgorithm algorithm;

        #endregion

        #region Constructors

        public MediumConfiguration(CacheMediumType type, CacheCapacity capacity, IReplacementAlgorithm algorithm)
        {
            this.type = type;

            this.capacity = capacity;

            this.algorithm = algorithm;
        }

        #endregion

        #region Properties

        public CacheMediumType Type
        {
            get
            {
                return this.type;
            }
        }

        public CacheCapacity Capacity
        {
            get
            {
                return this.capacity;
            }
        }

        public IReplacementAlgorithm Algorithm
        {
            get
            {
                return this.algorithm;
            }
        }

        #endregion

        #region Public Methods

        public void UpdateCacheCapacity(CacheCapacity capacity)
        {
            this.capacity = capacity;
        }

        public void UpdateAlgorithm(IReplacementAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        #endregion
    }
}
