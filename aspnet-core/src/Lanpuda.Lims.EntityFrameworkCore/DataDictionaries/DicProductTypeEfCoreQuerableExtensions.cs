using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.DataDictionaries;

/// <summary>
/// 
/// </summary>
public static class DicProductTypeEfCoreQueryableExtensions
{
    public static IQueryable<DicProductType> IncludeDetails(this IQueryable<DicProductType> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
              .Include(x => x.Creator)
              .Include(x => x.LastModifier)
            ;
    }
}
