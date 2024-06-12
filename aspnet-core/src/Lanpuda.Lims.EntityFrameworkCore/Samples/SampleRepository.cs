using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Samples;

public class SampleRepository : EfCoreRepository<LimsDbContext, Sample, Guid>, ISampleRepository
{
    public SampleRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Sample>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}