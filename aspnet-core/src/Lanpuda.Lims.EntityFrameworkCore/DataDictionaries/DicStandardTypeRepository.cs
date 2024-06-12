using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.DataDictionaries;

public class DicStandardTypeRepository : EfCoreRepository<LimsDbContext, DicStandardType, int>, IDicStandardTypeRepository
{
    public DicStandardTypeRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<DicStandardType>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}