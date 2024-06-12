using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Warehouses;

public class WarehouseRepository : EfCoreRepository<LimsDbContext, Warehouse, Guid>, IWarehouseRepository
{
    public WarehouseRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Warehouse>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}