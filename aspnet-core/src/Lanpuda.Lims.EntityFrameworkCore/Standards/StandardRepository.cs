using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Standards;

public class StandardRepository : EfCoreRepository<LimsDbContext, Standard, Guid>, IStandardRepository
{
    public StandardRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Standard>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}