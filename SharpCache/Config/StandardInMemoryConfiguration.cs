namespace SharpCache.Config
{
    #region Using Directives
    using SharpCache.Algorithms;
    #endregion

    public class StandardInMemoryConfiguration
    {
        #region Constructors

        private StandardInMemoryConfiguration()
        {
        }

        #endregion

        #region Public Methods

        public static CacheConfiguration Create()
        {
            ConfigurationInfo config = new ConfigurationInfo();

            ConfigNode node = new ConfigNode();
            node.Level = 1;
            node.Type = Mediums.CacheMediumType.InMemory;
            node.Capacity = new CacheCapacity(Primary.COUNTONLY, 3000);
            node.Algorithm = new LRUReplacementAlgorithm();

            config.Add(node);

            return CacheConfigurationBuilder.GenerateCacheConfiguration(config);
        }

        #endregion
    }
}
