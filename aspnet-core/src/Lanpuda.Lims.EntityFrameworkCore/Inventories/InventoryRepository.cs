using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Inventories;

public class InventoryRepository : EfCoreRepository<LimsDbContext, Inventory, Guid>, IInventoryRepository
{
    public InventoryRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Inventory>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }


    public async Task<Inventory> FindExistingAsync(Guid productId, Guid locationId, string lotNumber)
    {
        var queryable = await GetQueryableAsync();
        Inventory inventory = queryable.Where(m => m.ProductId == productId && m.LocationId == locationId && m.LotNumber == lotNumber).FirstOrDefault();
        return inventory;
    }

    public async Task<double> GetSumQuantityByProductIdAsync(Guid productId)
    {
        var queryable = await GetQueryableAsync();

        queryable = queryable.Where(m => m.ProductId == productId);

        double res = await queryable.SumAsync(m => m.Quantity);

        return res;

    }


    /// <summary>
    /// 入库
    /// </summary>
    /// <param name="locationId"></param>
    /// <param name="productId"></param>
    /// <param name="quantity"></param>
    /// <param name="lotNumber"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public async Task<Inventory> StorageAsync(Guid locationId, Guid productId, double quantity, string lotNumber)
    {
        if (quantity <= 0)
        {
            throw new UserFriendlyException("数量必须大于0");
        }
        var dbset = await GetDbSetAsync();
        var queryable = await GetQueryableAsync();
        Inventory inventory = queryable.Where(m => m.ProductId == productId && m.LocationId == locationId && m.LotNumber == lotNumber).FirstOrDefault();
        if (inventory != null)
        {
            inventory.Quantity = inventory.Quantity + quantity;
            dbset.Update(inventory);
            return inventory;
        }
        else
        {
            Inventory newInventory = new Inventory(GuidGenerator.Create());
            newInventory.LocationId = locationId;
            newInventory.ProductId = productId;
            newInventory.Quantity = quantity;
            newInventory.LotNumber = lotNumber;
            await dbset.AddAsync(newInventory);
            return newInventory;
        }
    }

    /// <summary>
    /// 出库
    /// </summary>
    /// <param name="locationId">库位id</param>
    /// <param name="productId">产品id</param>
    /// <param name="quantity">入库数量</param>
    /// <param name="lotNumber">入库批次</param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task<double> OutAsync(Guid locationId, Guid productId, double quantity, string lotNumber)
    {
        if (quantity <= 0) { throw new UserFriendlyException("数量必须大于0"); }
        var queryable = await WithDetailsAsync();
        var dbSet = await GetDbSetAsync();
        Inventory inventory = queryable.Where(m => m.LocationId == locationId && m.ProductId == productId && m.LotNumber == lotNumber).FirstOrDefault();
        if (inventory == null)
        {
            throw new UserFriendlyException("库存不存在");
        }
        //减库存
        inventory.Quantity = inventory.Quantity - quantity;
        if (inventory.Quantity < 0)
        {
            throw new UserFriendlyException(inventory.Product.Name + "库存不足");
        }
        else if (inventory.Quantity == 0) //出库后库存为零删除这条记录
        {
            dbSet.Remove(inventory);
            return 0;
        }
        else
        {
            dbSet.Update(inventory);
            return inventory.Quantity;
        }
    }
}