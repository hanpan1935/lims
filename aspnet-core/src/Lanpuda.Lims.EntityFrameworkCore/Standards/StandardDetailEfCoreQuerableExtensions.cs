using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Standards;

/// <summary>
/// 
/// </summary>
public static class StandardDetailEfCoreQueryableExtensions
{
    public static IQueryable<StandardDetail> IncludeDetails(this IQueryable<StandardDetail> queryable, bool include = true)
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
