using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.DataDictionaries;

/// <summary>
/// 
/// </summary>
public static class DicSampleTypeEfCoreQueryableExtensions
{
    public static IQueryable<DicSampleType> IncludeDetails(this IQueryable<DicSampleType> queryable, bool include = true)
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
