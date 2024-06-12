using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.InventoryOuts;

public class InventoryOutRepository : EfCoreRepository<LimsDbContext, InventoryOut, Guid>, IInventoryOutRepository
{
    public InventoryOutRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<InventoryOut>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}