﻿using Volo.Abp.Modularity;

namespace Lanpuda.Lims;

[DependsOn(
    typeof(LimsApplicationModule),
    typeof(LimsDomainTestModule)
)]
public class LimsApplicationTestModule : AbpModule
{

}
