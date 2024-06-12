using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Standards;

public class StandardDetailRepository : EfCoreRepository<LimsDbContext, StandardDetail, Guid>, IStandardDetailRepository
{
    public StandardDetailRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<StandardDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}