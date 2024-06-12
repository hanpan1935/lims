using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Records;

/// <summary>
/// 
/// </summary>
public static class RecordDetailEfCoreQueryableExtensions
{
    public static IQueryable<RecordDetail> IncludeDetails(this IQueryable<RecordDetail> queryable, bool include = true)
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
