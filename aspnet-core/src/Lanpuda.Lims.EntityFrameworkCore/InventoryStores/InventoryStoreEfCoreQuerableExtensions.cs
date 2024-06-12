using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.InventoryStores;

/// <summary>
/// 
/// </summary>
public static class InventoryStoreEfCoreQueryableExtensions
{
    public static IQueryable<InventoryStore> IncludeDetails(this IQueryable<InventoryStore> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
               .Include(x => x.Details).ThenInclude(m => m.Location).ThenInclude(m=>m.Warehouse) 
               .Include(m=>m.Details).ThenInclude(m => m.Product)
               .Include(x => x.Creator)
               .Include(x => x.LastModifier)
            ;
    }
}
