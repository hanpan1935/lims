using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.InventoryOuts;

/// <summary>
/// 
/// </summary>
public static class InventoryOutEfCoreQueryableExtensions
{
    public static IQueryable<InventoryOut> IncludeDetails(this IQueryable<InventoryOut> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
             .Include(x => x.Details).ThenInclude(m => m.Location).ThenInclude(m => m.Warehouse)
             .Include(x => x.Details).ThenInclude(m => m.Product)
             .Include(x => x.Creator)
             .Include(x => x.LastModifier)
             ;
    }
}
