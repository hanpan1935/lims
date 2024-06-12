using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.DataDictionaries;

public class DicProductTypeRepository : EfCoreRepository<LimsDbContext, DicProductType, int>, IDicProductTypeRepository
{
    public DicProductTypeRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<DicProductType>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}