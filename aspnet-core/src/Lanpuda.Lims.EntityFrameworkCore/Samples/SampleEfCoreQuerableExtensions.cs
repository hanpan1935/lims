using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Samples;

/// <summary>
/// 
/// </summary>
public static class SampleEfCoreQueryableExtensions
{
    public static IQueryable<Sample> IncludeDetails(this IQueryable<Sample> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Product) 
            .Include(x => x.DicSampleType)
            .Include(x => x.DicSampleProperty)
            .Include(x => x.Customer)
            .Include(x => x.Supplier)
            .Include(x => x.Creator)
            .Include(x => x.LastModifier)
            ;
    }
}
