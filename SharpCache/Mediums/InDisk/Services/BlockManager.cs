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
            else
            {
                int first1 = current.Value.First;
                int last1 = number - 1;

                int first2 = number + 1;
                int last2 = current.Value.Last;

                if (first1 <= last1 && first2 <= last2)
                {
                    this.SplitCurrentBlock(current, first1, last1, first2, last2);
                }
                else if (first1 > last1)
                {
                    this.UpdateCurrentBlock(current, first2, last2);
                }
                else if (first2 > last2)
                {
                    this.UpdateCurrentBlock(current, first1, last1);
                }
                else
                {
                    throw new Exception("can never reach here.");
                }
            }

            return number;
        }

        private void RemoveCurrentBlock(LinkedListNode<Block> block)
        {
            this.blocks.Remove(block);
        }

        private void SplitCurrentBlock(LinkedListNode<Block> block, int first1, int last1, int first2, int last2)
        {
            block.Value.First = first1;
            block.Value.Last = last1;

            this.blocks.AddAfter(block, new Block(first2,last2));
        }

        private void UpdateCurrentBlock(LinkedListNode<Block> block, int first, int last)
        {
            block.Value.First = first;
            block.Value.Last = last;
        }

        #endregion
    }
}
