using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.InventoryOuts;

public class InventoryOutDetailRepository : EfCoreRepository<LimsDbContext, InventoryOutDetail, Guid>, IInventoryOutDetailRepository
{
    public InventoryOutDetailRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<InventoryOutDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}