using System;
using BYSCORE.Common;
using BYSCORE.Common.Cache.Redis;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace BYSCORE.UI.Common.Extensions
{
    public static class CacheServiceExtensions
    {
        public static void AddCache(this IServiceCollection services, string redisCon, string instanceName, bool isRedis = false)
        {
            //注册缓存服务
            services.AddMemoryCache();
            if (isRedis)
            {
                //Use Redis
                services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions
                {
                    Configuration = redisCon,
                    InstanceName = instanceName
                }, 0));
            }
            else
            {
                //Use MemoryCache
                services.AddSingleton<IMemoryCache>(factory =>
                {
                    var cache = new MemoryCache(new MemoryCacheOptions());
                    return cache;
                });
                services.AddSingleton<ICacheService, MemoryCacheService>();
            }
        }
    }
}
