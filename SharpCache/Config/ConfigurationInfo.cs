namespace SharpCache.Config
{
    #region Using Directives
    using System.Collections;
    using System.Collections.Generic;
    #endregion

    public class ConfigurationInfo : IEnumerable<ConfigNode>
    {
        #region Fields
        
        private List<ConfigNode> nodes;

        #endregion

        #region Constructors

        public ConfigurationInfo()
        {
            this.nodes = new List<ConfigNode>();
        }

        #endregion

        #region Public Methods

        public void Add(ConfigNode node)
        {
            this.nodes.Add(node);
        }

        public void Remove(ConfigNode node)
        {
            this.nodes.Remove(node);
        }

        public void Clear()
        {
            this.nodes.Clear();
        }

        public IEnumerator<ConfigNode> GetEnumerator()
        {
            foreach (ConfigNode node in this.nodes)
            {
                yield return node;
            }
        }

        #endregion        

        #region Private Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
