using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.InventoryLogs;

public class InventoryLogRepository : EfCoreRepository<LimsDbContext, InventoryLog, Guid>, IInventoryLogRepository
{
    public InventoryLogRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<InventoryLog>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}