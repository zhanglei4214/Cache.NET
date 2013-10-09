namespace SharpCache.Common
{
    #region Using Directives
    using System;
    #endregion

    internal static class Ensure
    {
        public static void ArgumentNotNull(object argument, string name)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name, "Cannot be null");
            }
        }

        public static void ArgumentNotNullOrEmpty(string argument, string name)
        {
            if (String.IsNullOrEmpty(argument))
            {
                throw new ArgumentException("Cannot be null or empty", name);
            }
        }
    }
}
