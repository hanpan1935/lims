using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;

namespace Lanpuda.UniqueCode
{
    [DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
    public class UniqueCodeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
