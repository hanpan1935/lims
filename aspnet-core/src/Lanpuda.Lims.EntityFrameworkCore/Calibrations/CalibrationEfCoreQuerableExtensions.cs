using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Calibrations;

/// <summary>
/// 
/// </summary>
public static class CalibrationEfCoreQueryableExtensions
{
    public static IQueryable<Calibration> IncludeDetails(this IQueryable<Calibration> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Equipment) // TODO: AbpHelper generated
            .Include(x => x.Creator)
            .Include(x => x.LastModifier)
            ;
    }
}
