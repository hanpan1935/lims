using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Products;

/// <summary>
/// 
/// </summary>
public static class ProductEfCoreQueryableExtensions
{
    public static IQueryable<Product> IncludeDetails(this IQueryable<Product> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.DicProductType) 
            .Include(x => x.Creator)
            .Include(x => x.LastModifier)
            .Include(m=>m.Standard)
            ;
    }
}
