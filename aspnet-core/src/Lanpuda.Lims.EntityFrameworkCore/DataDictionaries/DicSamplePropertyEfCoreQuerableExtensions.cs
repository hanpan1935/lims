using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.DataDictionaries;

/// <summary>
/// 
/// </summary>
public static class DicSamplePropertyEfCoreQueryableExtensions
{
    public static IQueryable<DicSampleProperty> IncludeDetails(this IQueryable<DicSampleProperty> queryable, bool include = true)
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
