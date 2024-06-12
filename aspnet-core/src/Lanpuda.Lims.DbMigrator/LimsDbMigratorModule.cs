using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Lanpuda.Lims.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LimsEntityFrameworkCoreModule),
    typeof(LimsApplicationContractsModule)
    )]
public class LimsDbMigratorModule : AbpModule
{
}
