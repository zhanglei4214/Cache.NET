namespace SharpCache.Common
{
    #region Using Directives
    using System;
    #endregion

    internal class Toolkit
    {
        public static int ConvertToIntegar(byte[] bytes)
        {
            Ensure.ArgumentNotNull(bytes, "bytes");

            // If the system architecture is little-endian (that is, little end first), 
            // reverse the byte array. 
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
