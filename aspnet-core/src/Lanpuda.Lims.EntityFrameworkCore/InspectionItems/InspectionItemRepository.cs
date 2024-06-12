using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.InspectionItems;

public class InspectionItemRepository : EfCoreRepository<LimsDbContext, InspectionItem, Guid>, IInspectionItemRepository
{
    public InspectionItemRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<InspectionItem>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}