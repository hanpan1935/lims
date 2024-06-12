using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.Lims.Customers;

/// <summary>
/// 
/// </summary>
public static class CustomerEfCoreQueryableExtensions
{
    public static IQueryable<Customer> IncludeDetails(this IQueryable<Customer> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Creator) // TODO: AbpHelper generated
            .Include(x => x.LastModifier)

            ;
    }
}
