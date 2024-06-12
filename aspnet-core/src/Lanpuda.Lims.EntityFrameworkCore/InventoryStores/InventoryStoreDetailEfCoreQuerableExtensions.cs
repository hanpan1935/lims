using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.InventoryStores;

/// <summary>
/// 
/// </summary>
public static class InventoryStoreDetailEfCoreQueryableExtensions
{
    public static IQueryable<InventoryStoreDetail> IncludeDetails(this IQueryable<InventoryStoreDetail> queryable, bool include = true)
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
