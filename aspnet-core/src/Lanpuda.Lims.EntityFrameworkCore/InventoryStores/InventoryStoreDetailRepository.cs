using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.InventoryStores;

public class InventoryStoreDetailRepository : EfCoreRepository<LimsDbContext, InventoryStoreDetail, Guid>, IInventoryStoreDetailRepository
{
    public InventoryStoreDetailRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<InventoryStoreDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}