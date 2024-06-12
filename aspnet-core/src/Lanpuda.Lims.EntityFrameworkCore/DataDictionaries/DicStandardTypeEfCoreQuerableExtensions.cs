using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.DataDictionaries;

/// <summary>
/// 
/// </summary>
public static class DicStandardTypeEfCoreQueryableExtensions
{
    public static IQueryable<DicStandardType> IncludeDetails(this IQueryable<DicStandardType> queryable, bool include = true)
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
