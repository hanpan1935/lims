using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.Products;

/// <summary>
/// 
/// </summary>
public interface IProductRepository : IRepository<Product, Guid>
{
}
