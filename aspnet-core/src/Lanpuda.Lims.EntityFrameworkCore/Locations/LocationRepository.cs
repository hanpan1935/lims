using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Locations;

public class LocationRepository : EfCoreRepository<LimsDbContext, Location, Guid>, ILocationRepository
{
    public LocationRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Location>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}