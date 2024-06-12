using Lanpuda.Lims.Samples;
using Xunit;

namespace Lanpuda.Lims.EntityFrameworkCore.Domains;

[Collection(LimsTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<LimsEntityFrameworkCoreTestModule>
{

}
