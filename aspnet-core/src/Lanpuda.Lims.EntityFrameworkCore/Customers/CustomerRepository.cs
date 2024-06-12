using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Customers;

public class CustomerRepository : EfCoreRepository<LimsDbContext, Customer, Guid>, ICustomerRepository
{
    public CustomerRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Customer>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}