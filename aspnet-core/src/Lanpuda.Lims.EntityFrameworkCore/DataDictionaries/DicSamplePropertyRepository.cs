using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.DataDictionaries;

public class DicSamplePropertyRepository : EfCoreRepository<LimsDbContext, DicSampleProperty, int>, IDicSamplePropertyRepository
{
    public DicSamplePropertyRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<DicSampleProperty>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}