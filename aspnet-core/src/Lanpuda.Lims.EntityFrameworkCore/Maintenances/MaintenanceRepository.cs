using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Maintenances;

public class MaintenanceRepository : EfCoreRepository<LimsDbContext, Maintenance, Guid>, IMaintenanceRepository
{
    public MaintenanceRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Maintenance>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}