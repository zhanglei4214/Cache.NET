namespace SharpCache.Config
{
    #region Using Directives
    using System.Collections.Generic;
    using SharpCache.Schedulers;
    #endregion

    public class ConfigurationInfo
    {
        #region Fields

        private SchedulerType schedulerType;

        private List<ConfigNode> nodes;

        #endregion

        #region Constructors

        public ConfigurationInfo()
        {
            this.nodes = new List<ConfigNode>();
        }

        #endregion

        #region Properties

        public SchedulerType SchedulerType
        {
            get
            {
                return this.schedulerType;
            }
        }

        #endregion

        #region Public Methods

        public void Add(ConfigNode node)
        {
            this.nodes.Add(node);

            if (this.nodes.Count == 1)
            {
                this.schedulerType = this.nodes[0].Type;
            }
            else
            {
                this.schedulerType = Schedulers.SchedulerType.RAMFileScheduler;
            }
        }

        public void Remove(ConfigNode node)
        {
            this.nodes.Remove(node);
        }

        public void Clear()
        {
            this.nodes.Clear();
        }

        #endregion
    }
}
