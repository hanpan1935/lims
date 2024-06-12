using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Equipments;

/// <summary>
/// 
/// </summary>
public static class EquipmentEfCoreQueryableExtensions
{
    public static IQueryable<Equipment> IncludeDetails(this IQueryable<Equipment> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
              .Include(x => x.DicEquipmentType) // TODO: AbpHelper generated
              .Include(x => x.Creator)
              .Include(x => x.LastModifier)
            ;
    }
}
