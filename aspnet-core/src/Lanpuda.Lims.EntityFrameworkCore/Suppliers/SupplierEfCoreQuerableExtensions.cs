using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Suppliers;

/// <summary>
/// 
/// </summary>
public static class SupplierEfCoreQueryableExtensions
{
    public static IQueryable<Supplier> IncludeDetails(this IQueryable<Supplier> queryable, bool include = true)
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
