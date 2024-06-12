using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Customers.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using System.Collections.Generic;
using Lanpuda.Lims.Samples;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Customers;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class CustomerAppService : LimsAppService, ICustomerAppService
{

    private readonly ICustomerRepository _customerRepository;
    private readonly ISampleRepository _sampleRepository;

    public CustomerAppService(ICustomerRepository repository , ISampleRepository sampleRepository)
    {
        _customerRepository = repository;
        _sampleRepository = sampleRepository;
    }


    [Authorize(LimsPermissions.Customer_Create)]
    public async Task CreateAsync(CustomerCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        //new Customer and pass input to it
        var customer = ObjectMapper.Map<CustomerCreateDto, Customer>(input);
        await _customerRepository.InsertAsync(customer);
    }


    [Authorize(LimsPermissions.Customer_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Customer customer = await _customerRepository.FindAsync(id);
        if (customer == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        var sampleQuery = await _sampleRepository.GetQueryableAsync();
        var hasSample = sampleQuery.Any(x => x.CustomerId == id);
        if (hasSample)
        {
            throw new UserFriendlyException("无法删除,请先删除对应的样品!");
        }

        await _customerRepository.DeleteAsync(customer);
    }



    [Authorize(LimsPermissions.Customer_Default)]
    public async Task<CustomerDto> GetAsync(Guid id)
    {
        var result = await _customerRepository.FindAsync(id);
        return ObjectMapper.Map<Customer, CustomerDto>(result);
    }



    [Authorize(LimsPermissions.Customer_Default)]
    public async Task<PagedResultDto<CustomerDto>> GetPagedListAsync(CustomerGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _customerRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(!input.FullName.IsNullOrWhiteSpace(), x => x.FullName.Contains(input.FullName))
            .WhereIf(!input.ShortName.IsNullOrWhiteSpace(), x => x.ShortName.Contains(input.ShortName))
            .WhereIf(!input.Manager.IsNullOrWhiteSpace(), x => x.Manager.Contains(input.Manager))
            .WhereIf(!input.ManagerTel.IsNullOrWhiteSpace(), x => x.ManagerTel.Contains(input.ManagerTel))
            .WhereIf(!input.Remark.IsNullOrWhiteSpace(), x => x.Remark.Contains(input.Remark))
            .WhereIf(!input.Consignee.IsNullOrWhiteSpace(), x => x.Consignee.Contains(input.Consignee))
            .WhereIf(!input.ConsigneeTel.IsNullOrWhiteSpace(), x => x.ConsigneeTel.Contains(input.ConsigneeTel))
            .WhereIf(!input.Address.IsNullOrWhiteSpace(), x => x.Address.Contains(input.Address))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<CustomerDto>(totalCount, ObjectMapper.Map<List<Customer>, List<CustomerDto>>(result));
    }

    [Authorize(LimsPermissions.Customer_Update)]
    public async Task UpdateAsync(Guid id, CustomerUpdateDto input)
    {
        Customer customer = await _customerRepository.FindAsync(id);
        if (customer == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        customer.Number = input.Number;
        customer.FullName = input.FullName;
        customer.ShortName = input.ShortName;
        customer.Manager = input.Manager;
        customer.ManagerTel = input.ManagerTel;
        customer.Remark = input.Remark;
        customer.Consignee = input.Consignee;
        customer.ConsigneeTel = input.ConsigneeTel;
        customer.Address = input.Address;
        var result = await _customerRepository.UpdateAsync(customer);
    }
}
