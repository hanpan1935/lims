using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.DataDictionaries;

/// <summary>
/// 
/// </summary>
public static class DicEquipmentTypeEfCoreQueryableExtensions
{
    public static IQueryable<DicEquipmentType> IncludeDetails(this IQueryable<DicEquipmentType> queryable, bool include = true)
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
