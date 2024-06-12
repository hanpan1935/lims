using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Repairs;

/// <summary>
/// 
/// </summary>
public static class RepairEfCoreQueryableExtensions
{
    public static IQueryable<Repair> IncludeDetails(this IQueryable<Repair> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Equipment) // TODO: AbpHelper generated
            ;
    }
}
