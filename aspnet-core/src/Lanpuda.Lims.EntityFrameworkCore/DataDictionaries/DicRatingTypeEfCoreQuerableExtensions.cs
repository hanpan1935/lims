using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.DataDictionaries;

/// <summary>
/// 综合判级
/// </summary>
public static class DicRatingTypeEfCoreQueryableExtensions
{
    public static IQueryable<DicRatingType> IncludeDetails(this IQueryable<DicRatingType> queryable, bool include = true)
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
