namespace SharpCache.Mediums.InDisk.Services
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using SharpCache.Mediums.InDisk.DataStructures;
    #endregion

    internal class BlockManager
    {
        #region Fields

        public const int FAILED = -1;

        private const int MAX = int.MaxValue;

        private const int MIN = 0;

        private LinkedList<Block> blocks;

        #endregion

        #region Constructors

        public BlockManager()
        {
            this.blocks = new LinkedList<Block>();

            this.blocks.AddFirst(new Block(MIN,MAX));
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public int First()
        {
            return this.Get(this.blocks.First.Value.First);
        }

        public int Get(int number)
        {
            LinkedListNode<Block> first = this.blocks.First;
 
            for(LinkedListNode<Block> current = first;current != null;current = current.Next)
            {
                if (number >= current.Value.First && number <= current.Value.Last)
                {
                    return this.GetFromCurrentBlock(current, number);
                }
            }

            return FAILED;
        }

        public void Set(int number)
        {
        }

        #endregion

        #region Private Methods

        private int GetFromCurrentBlock(LinkedListNode<Block> current, int number)
        {
            if (current.Value.First == current.Value.Last)
            {
                this.RemoveCurrentBlock(current);
            }
            //// TODO: update the block info or split the block

            return number;
        }

        private void RemoveCurrentBlock(LinkedListNode<Block> block)
        {
            this.blocks.Remove(block);
        }

        #endregion
    }
}
