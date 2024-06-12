using Lanpuda.Lims.Samples;
using Xunit;

namespace Lanpuda.Lims.EntityFrameworkCore.Applications;

[Collection(LimsTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<LimsEntityFrameworkCoreTestModule>
{

}
