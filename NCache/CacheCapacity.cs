namespace NCache
{
    using System;

    public enum Primary
    {
        SIZEONLY,
        COUNTONLY,
        SIZECOUNT,
        COUNTSIZE
    }

    public class CacheCapacity
    {
        #region field

        private long size;

        private long count;

        private Primary primary;

        #endregion

        #region Properties

        public long Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        public long Count
        {
            get
            {
                return this.count;
            }
            set
            {
                this.count = value;
            }
        }

        public Primary Primary
        {
            get
            {
                return this.primary;
            }
            set
            {
                this.primary = value;
            }
        }

        #endregion

        #region Constructor

        public CacheCapacity(Primary primary)
        {
            ////TODO: the default value should be changed after designing cache schedule algorithm
            this.Size = 0;
            this.Count = 0;
            this.Primary = primary;
        }

        public CacheCapacity(Primary primary,long val)
        {
            this.Primary = primary;

            if(this.Primary == Primary.COUNTONLY)
            {
                this.Count = val;
                this.Size = 0;
            }
            else if (this.Primary == Primary.SIZEONLY)
            {
                this.Size = val;
                this.Count = 0;
            }
            else
            {
                throw new Exception("Params don't match the Primary type");
            }

        }

        public CacheCapacity(Primary primary, long count, long size)
        {
            this.Primary = primary;
            if (this.Primary == Primary.SIZECOUNT || this.Primary == Primary.COUNTSIZE)
            {
                this.Count = count;
                this.Size = size;
            }
            else
            {
                throw new Exception("Params don't match the Primary type");
            }
        }

        #endregion

        #region Public Methods

        public void Increase(long val)
        {
            if (this.Primary == Primary.COUNTONLY)
            {
                this.Count += val;
            }
            else if (this.Primary == Primary.SIZEONLY)
            {
                this.Size += val;
            }
            else
            {
                throw new Exception("Params don't match the Primary type");
            }
        }

        public void Increase(long size,long count)
        {
            if (this.Primary == Primary.SIZECOUNT || this.Primary == Primary.COUNTSIZE)
            {
                this.Count += count;
                this.Size += size;
            }
            else
            {
                throw new Exception("Params don't match the Primary type");
            }
        }

        public void Decrease(long val)
        {
            if (this.Primary == Primary.COUNTONLY)
            {
                this.Count -= val;
            }
            else if (this.Primary == Primary.SIZEONLY)
            {
                this.Size -= val;
            }
            else
            {
                throw new Exception("Params don't match the Primary type");
            }
        }

        public void Decrease(long size, long count)
        {
            if (this.Primary == Primary.SIZECOUNT || this.Primary == Primary.COUNTSIZE)
            {
                if (this.Count >= count && this.Size >= size)
                {
                    this.Count -= count;
                    this.Size -= size;
                }
                else
                {
                    throw new NotSupportedException("Invalid Operation");
                }
            }
            else
            {
                throw new Exception("Params don't match the Primary type");
            }
        }

        public bool SizeEquals(CacheCapacity cachecapacity)
        {
            if(this.Size == cachecapacity.Size)
            {
                return true;
            }

            return false;
        }

        public bool CountEquals(CacheCapacity cachecapacity)
        {
            if (this.Count == cachecapacity.Count)
            {
                return true;
            }

            return false;
        }

        public bool Equals(CacheCapacity cachecapacity)
        {
            if(this.Primary != cachecapacity.Primary)
            {
                return false;
            }

            switch (this.Primary)
            {
                case Primary.SIZEONLY:
                    if (this.Size == cachecapacity.Size)
                    {
                        return true;
                    }
                    break;
                case Primary.COUNTONLY:
                    if (this.Count == cachecapacity.Count)
                    {
                        return true;
                    }
                    break;
                case Primary.COUNTSIZE:
                    if (this.Count == cachecapacity.Count && this.Size == cachecapacity.Size)
                    {
                        return true;
                    }
                    break;
                case Primary.SIZECOUNT:
                    if (this.Count == cachecapacity.Count && this.Size == cachecapacity.Size)
                    {
                        return true;
                    }
                    break;
                default:
                    throw new NotSupportedException("Not support");
            }

            return false;
        }

        public bool IsEmpty()
        {
            switch (this.Primary)
            {
                case Primary.SIZEONLY:
                    if (this.Size == 0)
                    {
                        return true;
                    }
                    break;
                case Primary.COUNTONLY:
                    if (this.Count == 0)
                    {
                        return true;
                    }
                    break;
                case Primary.SIZECOUNT:
                    if (this.Count == 0 && this.Size == 0)
                    {
                        return true;
                    }
                    break;
                case Primary.COUNTSIZE:
                    if (this.Count == 0 && this.Size == 0)
                    {
                        return true;
                    }
                    break;
                default:
                    throw new NotSupportedException("Not support");
            }

            return false;
        }

        public override string ToString()
        {
            switch (this.Primary)
            {
                case Primary.COUNTONLY:
                    return "(count only) " + this.count.ToString();
                case Primary.COUNTSIZE:
                    return "(count first, size second) count: " + this.count.ToString() + " size: " + this.size.ToString();
                case Primary.SIZECOUNT:
                    return "(size first, count second) size: " + this.size.ToString() + " count: " + this.count.ToString();
                case Primary.SIZEONLY:
                    return "(size only) " + this.size.ToString();
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion
    }
}
