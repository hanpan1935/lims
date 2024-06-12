using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Maintenances;

/// <summary>
/// 
/// </summary>
public static class MaintenanceEfCoreQueryableExtensions
{
    public static IQueryable<Maintenance> IncludeDetails(this IQueryable<Maintenance> queryable, bool include = true)
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
