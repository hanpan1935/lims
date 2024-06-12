using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Inventories;

/// <summary>
/// 
/// </summary>
public static class InventoryEfCoreQueryableExtensions
{
    public static IQueryable<Inventory> IncludeDetails(this IQueryable<Inventory> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Location).ThenInclude(m=>m.Warehouse)
            .Include(m=>m.Product)

            ;
    }
}
