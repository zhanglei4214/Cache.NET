﻿using System;
using System.IO;
using SharpCache.Interfaces;
using System.Threading;

namespace SharpCache.Example
{
    public class Program
    {
        static void Main(string[] args)
        {
            SimpleUsage();

            BenchmarkTest();

            ExpiredTest();

            Console.Write("Press any key to conitnue...");
            Console.ReadKey();
        }

        private static void SimpleUsage()
        {
            Console.WriteLine("Simple Usage");

            CacheManager cm = new CacheManager();

            string conf = Path.Combine(Environment.CurrentDirectory, "DefaultCacheConfiguration.xml");

            ICache cache = cm.Create("simple", conf);

            cache["key1"] = "value1";

            cache["key2"] = 2;

            cache[3] = "value3";

            cache[4] = 4;

            CacheValue value4 = cache[4];
            int v4 = (int)value4.Content;
        }

        private static void BenchmarkTest()
        {
            Console.WriteLine("Benchmark test");

            CacheManager cm = new CacheManager();

            string conf = Path.Combine(Environment.CurrentDirectory, "DefaultCacheConfiguration.xml");

            ICache cache = cm.Create("benchmark", conf);

            DateTime time1 = DateTime.Now;

            for (int i = 0; i < 10000000; i++)
            {
                cache[i] = i;
            }

            DateTime time2 = DateTime.Now;

            Console.WriteLine("insert 10000000 items need " + (time2 - time1).ToString()); 
        }

        private static void ExpiredTest()
        {
            Console.WriteLine("Expire test");

            CacheManager cm = new CacheManager();

            string conf = Path.Combine(Environment.CurrentDirectory, "DefaultCacheConfiguration.xml");

            ICache cache = cm.Create("expire", conf);

            CacheValue value = new CacheValue();
            value.Content = "expired";
            value.MetaData = new CacheItemMetaData(TimeSpan.FromSeconds(1).Ticks);

            cache[1] = value;

            CacheValue readBefore = cache[1];

            if (readBefore == null)
            {
                throw new Exception("readBefore should not be null.");
            }

            Thread.Sleep(new TimeSpan(0, 0, 2));

            CacheValue readAfter = cache[1];

            if (readAfter == null)
            {
                Console.WriteLine("data expires after 1 second as expected.");
            }
        }
    }
}