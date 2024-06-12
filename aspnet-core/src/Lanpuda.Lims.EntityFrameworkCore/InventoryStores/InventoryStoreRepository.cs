using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.InventoryStores;

public class InventoryStoreRepository : EfCoreRepository<LimsDbContext, InventoryStore, Guid>, IInventoryStoreRepository
{
    public InventoryStoreRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<InventoryStore>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}