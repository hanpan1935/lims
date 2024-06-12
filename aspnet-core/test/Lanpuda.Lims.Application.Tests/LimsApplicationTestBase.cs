using Volo.Abp.Modularity;

namespace Lanpuda.Lims;

public abstract class LimsApplicationTestBase<TStartupModule> : LimsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
