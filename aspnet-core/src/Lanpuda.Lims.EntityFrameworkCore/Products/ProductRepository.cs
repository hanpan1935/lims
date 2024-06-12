using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Products;

public class ProductRepository : EfCoreRepository<LimsDbContext, Product, Guid>, IProductRepository
{
    public ProductRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Product>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}