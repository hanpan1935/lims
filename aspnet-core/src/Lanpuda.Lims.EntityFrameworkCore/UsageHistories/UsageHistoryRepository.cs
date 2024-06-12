using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.UsageHistories;

public class UsageHistoryRepository : EfCoreRepository<LimsDbContext, UsageHistory, Guid>, IUsageHistoryRepository
{
    public UsageHistoryRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<UsageHistory>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}