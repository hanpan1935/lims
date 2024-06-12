using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.DataDictionaries;

public class DicRatingTypeRepository : EfCoreRepository<LimsDbContext, DicRatingType, int>, IDicRatingTypeRepository
{
    public DicRatingTypeRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<DicRatingType>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}