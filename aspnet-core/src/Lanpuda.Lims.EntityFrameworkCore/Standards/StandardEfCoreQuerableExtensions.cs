using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Standards;

/// <summary>
/// 
/// </summary>
public static class StandardEfCoreQueryableExtensions
{
    public static IQueryable<Standard> IncludeDetails(this IQueryable<Standard> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Details.OrderBy(m=>m.Sort)).ThenInclude(m=>m.InspectionItem) // TODO: AbpHelper generated
            .Include(m=>m.DicStandardType)
            .Include(x => x.Creator)
            .Include(x => x.LastModifier)
            ;
    }
}
