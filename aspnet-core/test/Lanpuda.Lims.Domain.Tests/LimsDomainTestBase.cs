using Volo.Abp.Modularity;

namespace Lanpuda.Lims;

/* Inherit from this class for your domain layer tests. */
public abstract class LimsDomainTestBase<TStartupModule> : LimsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
