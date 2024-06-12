using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Repairs.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Lanpuda.UniqueCode;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Repairs;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class RepairAppService : LimsAppService, IRepairAppService
{

    private readonly IRepairRepository _repairRepository;
    private readonly IUniqueCodeGenerator _uniqueCodeGenerator;

    public RepairAppService(IRepairRepository repository, IUniqueCodeGenerator uniqueCodeGenerator)
    {
        _repairRepository = repository;
        _uniqueCodeGenerator = uniqueCodeGenerator;
    }

    [Authorize(LimsPermissions.Repair_Create)]
    public async Task CreateAsync(RepairCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        //new Repair and pass input to it
        string number = await _uniqueCodeGenerator.GetUniqueNumberAsync(LimsNumberPrefix.RepairPrefix);
        var repair = new Repair(id, number);
        repair.EquipmentId = input.EquipmentId;
        repair.RepairDate = input.RepairDate;
        repair.RepairResult = input.RepairResult;
        repair.Description = input.Description;
        repair.Person = input.Person;
        repair.RepairTime = input.RepairTime;
        repair.RepairCost = input.RepairCost;
        repair.RepairWorkOrderNumber = input.RepairWorkOrderNumber;
        repair.RepairDepartment = input.RepairDepartment;
        repair.RepairStandard = input.RepairStandard;
        repair.ConfirmPerson = input.ConfirmPerson;
        repair.Remark = input.Remark;
       


        await _repairRepository.InsertAsync(repair);
    }


    [Authorize(LimsPermissions.Repair_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Repair repair = await _repairRepository.FindAsync(id);
        if (repair == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _repairRepository.DeleteAsync(repair);
    }

    [Authorize(LimsPermissions.Repair_Default)]
    public async Task<RepairDto> GetAsync(Guid id)
    {
        var result = await _repairRepository.FindAsync(id);
        return ObjectMapper.Map<Repair, RepairDto>(result);
    }

    [Authorize(LimsPermissions.Repair_Default)]
    public async Task<PagedResultDto<RepairDto>> GetPagedListAsync(RepairGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repairRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.EquipmentId != null, x => x.EquipmentId == input.EquipmentId)
            .WhereIf(input.RepairDate != null, x => x.RepairDate == input.RepairDate)
            .WhereIf(input.RepairResult != null, x => x.RepairResult == input.RepairResult)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<RepairDto>(totalCount, ObjectMapper.Map<List<Repair>, List<RepairDto>>(result));
    }

    [Authorize(LimsPermissions.Repair_Update)]
    public async Task UpdateAsync(Guid id, RepairUpdateDto input)
    {
        Repair repair = await _repairRepository.FindAsync(id);
        if (repair == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        repair.EquipmentId = input.EquipmentId;
        repair.RepairDate = input.RepairDate;
        repair.RepairResult = input.RepairResult;
        repair.Description = input.Description;
        repair.Person = input.Person;
        repair.RepairTime = input.RepairTime;
        repair.RepairCost = input.RepairCost;
        repair.RepairWorkOrderNumber = input.RepairWorkOrderNumber;
        repair.RepairDepartment = input.RepairDepartment;
        repair.RepairStandard = input.RepairStandard;
        repair.ConfirmPerson = input.ConfirmPerson;
        repair.Remark = input.Remark;
        var result = await _repairRepository.UpdateAsync(repair);
    }
}
