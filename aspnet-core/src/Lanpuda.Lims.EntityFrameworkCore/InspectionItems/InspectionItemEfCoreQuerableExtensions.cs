using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.InspectionItems;

/// <summary>
/// 
/// </summary>
public static class InspectionItemEfCoreQueryableExtensions
{
    public static IQueryable<InspectionItem> IncludeDetails(this IQueryable<InspectionItem> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.DefaultEquipment)
            .Include(x => x.Creator)
            .Include(x => x.LastModifier)
            ;
    }
}
