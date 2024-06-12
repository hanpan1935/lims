using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.UsageHistories;

/// <summary>
/// 
/// </summary>
public static class UsageHistoryEfCoreQueryableExtensions
{
    public static IQueryable<UsageHistory> IncludeDetails(this IQueryable<UsageHistory> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Equipment) // TODO: AbpHelper generated
            .Include(x => x.Creator)
            .Include(x => x.LastModifier)
            ;
    }
}
