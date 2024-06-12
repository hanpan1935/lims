using Volo.Abp.Modularity;

namespace Lanpuda.Lims;

[DependsOn(
    typeof(LimsDomainModule),
    typeof(LimsTestBaseModule)
)]
public class LimsDomainTestModule : AbpModule
{

}
