using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.Customers;

/// <summary>
/// 
/// </summary>
public interface ICustomerRepository : IRepository<Customer, Guid>
{
}
