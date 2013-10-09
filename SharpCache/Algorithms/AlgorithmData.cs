namespace SharpCache.Algorithms
{
    #region Using Directives
    using System.Collections.Generic;
    #endregion

    public class AlgorithmData
    {
        #region Fields

        private Dictionary<CacheKey, LinkedListNode<CacheSummary>> dataInDict;

        private LinkedList<CacheSummary> dataInList;

        #endregion

        #region Constructors

        public AlgorithmData()
        {
            this.dataInDict = new Dictionary<CacheKey, LinkedListNode<CacheSummary>>();
            this.dataInList = new LinkedList<CacheSummary>();
        }

        #endregion

        #region Public Methods

        public CacheKey RemoveFromTail()
        {
            CacheKey key;
            key = this.dataInList.Last.Value.Key;
            
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

            LinkedListNode<CacheSummary> node = new LinkedListNode<CacheSummary>(summary);

            // add key & node pair into dictionary
            this.dataInDict.Add(summary.Key, node);

            // create new node and put it to the head of double link list
            this.dataInList.AddFirst(node);
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

            LinkedListNode<CacheSummary> node = this.dataInDict[key];

            // put node to the head of double link list
            this.dataInList.Remove(node);
            this.dataInList.AddFirst(node);
        }

        public void Remove(CacheKey key)
        {
            if (key == null || this.dataInDict.ContainsKey(key) == false)
            {
                return;
            }

            LinkedListNode<CacheSummary> node = this.dataInDict[key];

            this.dataInList.Remove(node);

            this.dataInDict.Remove(key);
        }

        #endregion
    }
}
