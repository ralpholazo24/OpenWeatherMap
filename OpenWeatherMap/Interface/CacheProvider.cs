using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Interface
{
    public class CacheProvider
    {
        private static IDistributedCache distributedCache;
		public static void RegisterCacheProvider(IDistributedCache _distributedCache)
		{
            distributedCache = _distributedCache;
		}

		public static IDistributedCache GetCacheProvider()
		{
			return distributedCache;
		}
	}
}
