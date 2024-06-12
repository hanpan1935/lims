using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Records;

/// <summary>
/// 
/// </summary>
public static class RecordEfCoreQueryableExtensions
{
    public static IQueryable<Record> IncludeDetails(this IQueryable<Record> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Sample).ThenInclude(m => m.Product)
            .Include(x => x.Sample).ThenInclude(m => m.DicSampleType)
            .Include(x => x.Sample).ThenInclude(m => m.DicSampleProperty)
            .Include(x => x.Sample).ThenInclude(m => m.Supplier)
            .Include(x => x.Sample).ThenInclude(m => m.Customer)
            .Include(x => x.DicRatingType)
            .Include(x => x.Details).ThenInclude(m => m.InspectionItem).ThenInclude(m=>m.DefaultEquipment)
            .Include(x => x.Creator)
            .Include(x => x.LastModifier)
            ;
    }
}
