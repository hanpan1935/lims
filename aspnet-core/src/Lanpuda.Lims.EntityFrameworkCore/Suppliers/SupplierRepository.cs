using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Suppliers;

public class SupplierRepository : EfCoreRepository<LimsDbContext, Supplier, Guid>, ISupplierRepository
{
    public SupplierRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Supplier>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}