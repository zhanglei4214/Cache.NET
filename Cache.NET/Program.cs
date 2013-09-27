using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SharpCache;

namespace Cache.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            CacheManager cm = new CacheManager();

            string conf = Path.Combine(Environment.CurrentDirectory, "DefaultCacheConfiguration.xml");

            var ff = cm.Create("ff", conf);
        }
    }
}
