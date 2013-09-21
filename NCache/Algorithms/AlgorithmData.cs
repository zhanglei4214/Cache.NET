namespace NCache.Algorithms
{
    #region Using Directives
    using System.Collections.Generic;
    using NCache.Common.DataStructures;
    using NCache.Common.Interfaces;
    #endregion

    public class AlgorithmData
    {
        #region Fields

        private Dictionary<CacheKey, Node<CacheSummary>> dataInDict;

        private IDoubleLinkList<CacheSummary> dataInList;

        #endregion

        #region Constructors

        public AlgorithmData()
        {
            this.dataInDict = new Dictionary<CacheKey, Node<CacheSummary>>();
            this.dataInList = new DoubleLinkList<CacheSummary>();
        }

        #endregion

        #region Public Methods

        public CacheKey RemoveFromTail()
        {
            CacheKey key;
            key = this.dataInList.Tail.Data.Key;

            //remove from the tail of the cache list, dictionary
            this.dataInList.RemoveLast();
            this.dataInDict.Remove(key);

            return key;
        }

        public void AddToHead(CacheSummary summary)
        {
            if (summary == null)
            {
                return;
            }

            Node<CacheSummary> node = new Node<CacheSummary>(summary);

            // add key & node pair into dictionary
            this.dataInDict.Add(summary.Key, node);

            // create new node and put it to the head of double link list
            this.dataInList.AddFirst(summary);
        }

        public void MoveToHead(CacheKey key)
        {
            if (key == null || this.dataInDict.ContainsKey(key) == false)
            {
                return;
            }

            if (this.dataInList.Count == 1)
            {
                // only current node in double link list, no need to update
                return;
            }

            Node<CacheSummary> node = this.dataInDict[key];

            // put node to the head of double link list
            this.dataInList.MoveToHead(node);
        }

        public void Remove(CacheKey key)
        {
            if (key == null || this.dataInDict.ContainsKey(key) == false)
            {
                return;
            }

            Node<CacheSummary> node = this.dataInDict[key];

            this.dataInList.Remove(node);

            this.dataInDict.Remove(key);
        }

        #endregion
    }
}
