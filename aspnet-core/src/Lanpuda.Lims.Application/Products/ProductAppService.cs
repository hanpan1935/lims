using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Products.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Lanpuda.Lims.Samples;
using Lanpuda.Lims.Inventories;
using Lanpuda.Lims.InventoryLogs;
using Lanpuda.Lims.InventoryStores;
using Lanpuda.Lims.InventoryOuts;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Products;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class ProductAppService : LimsAppService, IProductAppService
{

    private readonly IProductRepository _productRepository;
    private readonly ISampleRepository _sampleRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;
    private readonly IInventoryStoreDetailRepository _inventoryStoreDetailRepository;
    private readonly IInventoryOutDetailRepository _inventoryOutDetailRepository;


    public ProductAppService(
        IProductRepository repository, 
        ISampleRepository sampleRepository,
        IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository,
        IInventoryStoreDetailRepository inventoryStoreDetailRepository,
        IInventoryOutDetailRepository inventoryOutDetailRepository
        )
    {
        _productRepository = repository;
        _sampleRepository = sampleRepository;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
        _inventoryStoreDetailRepository = inventoryStoreDetailRepository;
        _inventoryOutDetailRepository = inventoryOutDetailRepository;
    }

    [Authorize(LimsPermissions.Product_Create)]
    public async Task CreateAsync(ProductCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        //new Product and pass input to it
        var product = ObjectMapper.Map<ProductCreateDto, Product>(input);
        await _productRepository.InsertAsync(product);
    }

    [Authorize(LimsPermissions.Product_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Product product = await _productRepository.FindAsync(id);
        if (product == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //判断是否存在样品
        var hasSample = await _sampleRepository.AnyAsync(x => x.ProductId == id);
        if (hasSample)
        {
            throw new UserFriendlyException(L["Message:CannotDelete"]);
        }
        //判断是否存在库存
        var hasInventory = await _inventoryRepository.AnyAsync(x => x.ProductId == id);
        if (hasInventory)
        {
            throw new UserFriendlyException(L["Message:CannotDelete"]);
        }

        //判断是否存在库存日志
        var hasInventoryLog = await _inventoryLogRepository.AnyAsync(x => x.ProductId == id);
        if (hasInventoryLog)
        {
            throw new UserFriendlyException(L["Message:CannotDelete"]);
        }

        //判断是否存在入库单
        var hasInventoryStoreDetail = await _inventoryStoreDetailRepository.AnyAsync(x => x.ProductId == id);
        if (hasInventoryStoreDetail)
        {
            throw new UserFriendlyException(L["Message:CannotDelete"]);
        }

        //判断是否存在出库单
        var hasInventoryOutDetail = await _inventoryOutDetailRepository.AnyAsync(x => x.ProductId == id);
        if (hasInventoryOutDetail)
        {
            throw new UserFriendlyException(L["Message:CannotDelete"]);
        }

        await _productRepository.DeleteAsync(product);
    }

    [Authorize(LimsPermissions.Product_Default)]
    public async Task<ProductDto> GetAsync(Guid id)
    {
        var result = await _productRepository.FindAsync(id);
        return ObjectMapper.Map<Product, ProductDto>(result);
    }

    [Authorize(LimsPermissions.Product_Default)]
    public async Task<PagedResultDto<ProductDto>> GetPagedListAsync(ProductGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _productRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Unit.IsNullOrWhiteSpace(), x => x.Unit.Contains(input.Unit))
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.DicProductTypeId != null, x => x.DicProductTypeId == input.DicProductTypeId)
            .WhereIf(!input.Spec.IsNullOrWhiteSpace(), x => x.Spec.Contains(input.Spec))
            .WhereIf(!input.Remark.IsNullOrWhiteSpace(), x => x.Remark.Contains(input.Remark))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<ProductDto>(totalCount, ObjectMapper.Map<List<Product>, List<ProductDto>>(result));
    }

    [Authorize(LimsPermissions.Product_Update)]
    public async Task UpdateAsync(Guid id, ProductUpdateDto input)
    {
        Product product = await _productRepository.FindAsync(id);
        if (product == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        product.Name = input.Name;
        product.Unit = input.Unit;
        product.Number = input.Number;
        product.DicProductTypeId = input.DicProductTypeId;
        product.Spec = input.Spec;
        product.StandardId = input.StandardId;
        product.Remark = input.Remark;
        var result = await _productRepository.UpdateAsync(product);
    }
}
