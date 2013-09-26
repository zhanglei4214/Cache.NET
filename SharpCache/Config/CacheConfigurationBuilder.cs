namespace SharpCache.Config
{
    #region Using Directives
    using System;
    using System.IO;
    using System.Xml;
    using SharpCache.Interfaces;
    using SharpCache.Schedulers;
    #endregion

    public class CacheConfigurationBuilder
    {
        #region Constructors

        private CacheConfigurationBuilder()
        {
        }

        #endregion

        #region Public Methods

        public static CacheConfiguration Build(string file)
        {
            if (string.IsNullOrEmpty(file) == true)
            {
                throw new NullReferenceException("file name is empty.");
            }

            if (File.Exists(file) == false)
            {
                throw new FileNotFoundException("cannot find file: " + file);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            return Build(doc);
        }

        public static CacheConfiguration Build(XmlDocument doc)
        {
            if (doc == null)
            {
                throw new NullReferenceException("XML document is null.");
            }

            ConfigurationInfo info = ParseXMLIntoConfigurationInfo(doc);

            CacheConfiguration configuration = new CacheConfiguration(info.SchedulerType);

            return configuration;
        }

        #endregion

        #region Private Methods

        private static ConfigurationInfo ParseXMLIntoConfigurationInfo(XmlDocument doc)
        {            
            XmlNode mediums = doc.SelectSingleNode("SharpCache/CacheMediums");
            if (mediums == null)
            {
                throw new NullReferenceException("SharpCache/CacheMediums element is missing.");
            }

            ConfigurationInfo collection = new ConfigurationInfo();

            foreach (XmlNode node in mediums.ChildNodes)
            {
                ConfigNode configNode = ParseConfigNode(node);
                if (configNode.Validate() == false)
                {
                    throw new Exception("Medium node is invalid.");
                }

                collection.Add(configNode);
            }

            return collection;
        }

        private static ConfigNode ParseConfigNode(XmlNode node)
        {
            ConfigNode configNode = new ConfigNode();
            XmlAttributeCollection attributes = node.Attributes;
            foreach (XmlAttribute attribute in attributes)
            {
                if (attribute.Name == "Level")
                {
                    configNode.Level = int.Parse(attribute.Value);
                }

                else if (attribute.Name == "Type")
                {
                    configNode.Type = (SchedulerType)Enum.Parse(typeof(SchedulerType), attribute.Value + "Scheduler");
                }
            }

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "ReplacementAlgorithm")
                {
                    configNode.Algorithm = (IReplacementAlgorithm)Activator.CreateInstance(Type.GetType("SharpCache.Algorithms." + child.InnerText));
                }

                else if (child.Name == "Capacity")
                {
                    configNode.Capacity = ParseCacheCapacity(child);
                }
            }

            return configNode;
        }

        private static CacheCapacity ParseCacheCapacity(XmlNode node)
        {
            XmlAttributeCollection attributes = node.Attributes;
            if (attributes == null || attributes.Count != 1)
            {
                throw new Exception("Capacity element is invalid.");
            }            

            if (attributes[0].Name == "Type")
            {
                CacheCapacity capacity = new CacheCapacity((Primary)Enum.Parse(typeof(Primary), attributes[0].Value));
                capacity.Count = long.Parse(node.InnerText);

                return capacity;
            }

            return null;
        }

        #endregion
    }
}
