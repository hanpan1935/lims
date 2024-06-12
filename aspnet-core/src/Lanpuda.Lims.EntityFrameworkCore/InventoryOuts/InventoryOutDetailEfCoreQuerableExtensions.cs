using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.InventoryOuts;

/// <summary>
/// 
/// </summary>
public static class InventoryOutDetailEfCoreQueryableExtensions
{
    public static IQueryable<InventoryOutDetail> IncludeDetails(this IQueryable<InventoryOutDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            // .Include(x => x.xxx) // TODO: AbpHelper generated
            .Include(x => x.Creator)
            .Include(x => x.LastModifier)
            ;
    }
}
