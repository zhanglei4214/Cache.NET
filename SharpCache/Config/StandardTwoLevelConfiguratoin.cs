namespace SharpCache.Config
{
    #region Using Directives
    using SharpCache.Algorithms;
    #endregion

    public class StandardTwoLevelConfiguratoin
    {
        #region Constructors

        private StandardTwoLevelConfiguratoin()
        {
        }

        #endregion

        #region Public Methods

        public static CacheConfiguration Create()
        {
            return Create(3000, 30000);
        }

        public static CacheConfiguration Create(int firstNumber, int secondNumber)
        {
            ConfigurationInfo config = new ConfigurationInfo();

            ConfigNode node1 = new ConfigNode();
            node1.Level = 1;
            node1.Type = Mediums.CacheMediumType.InMemory;
            node1.Capacity = new CacheCapacity(Primary.COUNTONLY, 3000);
            node1.Algorithm = new LRUReplacementAlgorithm();

            config.Add(node1);

            ConfigNode node2 = new ConfigNode();
            node2.Level = 2;
            node2.Type = Mediums.CacheMediumType.InDisk;
            node2.Capacity = new CacheCapacity(Primary.COUNTONLY, 30000);
            node2.Algorithm = new LRUReplacementAlgorithm();

            config.Add(node2);

            return CacheConfigurationBuilder.GenerateCacheConfiguration(config);
        }

        #endregion
    }
}
