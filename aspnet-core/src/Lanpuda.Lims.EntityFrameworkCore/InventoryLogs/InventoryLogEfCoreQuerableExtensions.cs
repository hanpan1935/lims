using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.InventoryLogs;

/// <summary>
/// 库存流水
/// </summary>
public static class InventoryLogEfCoreQueryableExtensions
{
    public static IQueryable<InventoryLog> IncludeDetails(this IQueryable<InventoryLog> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Product)
            .Include(x => x.Location).ThenInclude(m=>m.Warehouse)
            .Include(x => x.Creator)
            .Include(x => x.LastModifier)
            ;
    }
}
