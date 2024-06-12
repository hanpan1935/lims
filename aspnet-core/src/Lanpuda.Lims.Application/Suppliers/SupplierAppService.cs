using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Suppliers.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Lanpuda.Lims.Samples;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Suppliers;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class SupplierAppService : LimsAppService, ISupplierAppService
{

    private readonly ISupplierRepository _supplierRepository;
    private readonly ISampleRepository _sampleRepository;

    public SupplierAppService(ISupplierRepository repository,ISampleRepository sampleRepository)
    {
        _supplierRepository = repository;
        _sampleRepository = sampleRepository;
    }

    [Authorize(LimsPermissions.Supplier_Create)]
    public async Task CreateAsync(SupplierCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        //new Supplier and pass input to it
        var supplier = ObjectMapper.Map<SupplierCreateDto, Supplier>(input);
        await _supplierRepository.InsertAsync(supplier);
    }

    [Authorize(LimsPermissions.Supplier_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Supplier supplier = await _supplierRepository.FindAsync(id);
        if (supplier == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        var query = await _sampleRepository.GetQueryableAsync();
        var hasSample = query.Any(x => x.SupplierId == id);
        if (hasSample)
        {
            throw new UserFriendlyException("无法删除,请先删除对应的样品!");
        }

        await _supplierRepository.DeleteAsync(supplier);
    }

    [Authorize(LimsPermissions.Supplier_Default)]
    public async Task<SupplierDto> GetAsync(Guid id)
    {
        var result = await _supplierRepository.FindAsync(id);
        return ObjectMapper.Map<Supplier, SupplierDto>(result);
    }

    [Authorize(LimsPermissions.Supplier_Default)]
    public async Task<PagedResultDto<SupplierDto>> GetPagedListAsync(SupplierGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _supplierRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.FullName.IsNullOrWhiteSpace(), x => x.FullName.Contains(input.FullName))
            .WhereIf(!input.ShortName.IsNullOrWhiteSpace(), x => x.ShortName.Contains(input.ShortName))
            .WhereIf(!input.Manager.IsNullOrWhiteSpace(), x => x.Manager.Contains(input.Manager))
            .WhereIf(!input.ManagerTel.IsNullOrWhiteSpace(), x => x.ManagerTel.Contains(input.ManagerTel))
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(!input.Remark.IsNullOrWhiteSpace(), x => x.Remark.Contains(input.Remark))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<SupplierDto>(totalCount, ObjectMapper.Map<List<Supplier>, List<SupplierDto>>(result));
    }

    [Authorize(LimsPermissions.Supplier_Update)]
    public async Task UpdateAsync(Guid id, SupplierUpdateDto input)
    {
        Supplier supplier = await _supplierRepository.FindAsync(id);
        if (supplier == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        supplier.FullName = input.FullName;
        supplier.ShortName = input.ShortName;
        supplier.Manager = input.Manager;
        supplier.ManagerTel = input.ManagerTel;
        supplier.Number = input.Number;
        supplier.Remark = input.Remark;

        var result = await _supplierRepository.UpdateAsync(supplier);
    }
}
