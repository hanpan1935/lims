using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.InspectionTasks;

/// <summary>
/// 
/// </summary>
public static class InspectionTaskEfCoreQueryableExtensions
{
    public static IQueryable<InspectionTask> IncludeDetails(this IQueryable<InspectionTask> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Equipment)
            .Include(x => x.RecordDetail).ThenInclude(m => m.Record).ThenInclude(m => m.Sample).ThenInclude(m => m.Product)
            .Include(x => x.RecordDetail).ThenInclude(m => m.InspectionItem)
            .Include(x => x.Creator)
            .Include(x => x.LastModifier)
            ;
    }
}
