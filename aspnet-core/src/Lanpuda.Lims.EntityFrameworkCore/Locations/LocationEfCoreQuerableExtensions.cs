using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Locations;

/// <summary>
/// 
/// </summary>
public static class LocationEfCoreQueryableExtensions
{
    public static IQueryable<Location> IncludeDetails(this IQueryable<Location> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Warehouse) // TODO: AbpHelper generated
            .Include(x => x.Creator)
            .Include(x => x.LastModifier)
            ;
    }
}
