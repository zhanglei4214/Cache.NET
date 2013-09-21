namespace SharpCache.Hash
{
    #region using namespace
    #endregion

    public static class HashKeyGenerator
    {
        /// <summary>
        /// Adopts sdbm hash algorithm.
        /// Link: http://www.cse.yorku.ca/~oz/hash.html
        /// the actual function is hash(i) = hash(i - 1) * 65599 + str[i]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long CalculateHash(string str)
        {
            long hash = 0;
            foreach (char c in str)
            {
                hash = (int)c + (hash << 6) + (hash << 16) - hash;
            }

            return hash;
        }
    }
}
