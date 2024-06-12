using Xunit;

namespace Lanpuda.Lims.EntityFrameworkCore;

[CollectionDefinition(LimsTestConsts.CollectionDefinitionName)]
public class LimsEntityFrameworkCoreCollection : ICollectionFixture<LimsEntityFrameworkCoreFixture>
{

}
