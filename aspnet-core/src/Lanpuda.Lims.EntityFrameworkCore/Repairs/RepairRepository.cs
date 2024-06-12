using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Repairs;

public class RepairRepository : EfCoreRepository<LimsDbContext, Repair, Guid>, IRepairRepository
{
    public RepairRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Repair>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}