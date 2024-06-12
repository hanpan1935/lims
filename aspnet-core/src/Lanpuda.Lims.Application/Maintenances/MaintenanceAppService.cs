using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Maintenances.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Lanpuda.UniqueCode;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Maintenances;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class MaintenanceAppService : LimsAppService, IMaintenanceAppService
{

    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly IUniqueCodeGenerator _uniqueCodeGenerator;

    public MaintenanceAppService(IMaintenanceRepository repository, IUniqueCodeGenerator uniqueCodeGenerator)
    {
        _maintenanceRepository = repository;
        _uniqueCodeGenerator = uniqueCodeGenerator;
    }

    [Authorize(LimsPermissions.Maintenance_Create)]
    public async Task CreateAsync(MaintenanceCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeGenerator.GetUniqueNumberAsync(LimsNumberPrefix.MaintenancePrefix);
        var maintenance = new Maintenance(id, number);
        maintenance.EquipmentId = input.EquipmentId;
        maintenance.Date = input.Date;
        maintenance.MaintenanceType = input.MaintenanceType;
        maintenance.Description = input.Description;
        maintenance.Person = input.Person;
        maintenance.SpentTime = input.SpentTime;
        maintenance.Cost = input.Cost;
        maintenance.Result = input.Result;
        maintenance.WorkOrderNumber = input.WorkOrderNumber;
        maintenance.Department = input.Department;
        maintenance.Remark = input.Remark;

        await _maintenanceRepository.InsertAsync(maintenance);
    }

    [Authorize(LimsPermissions.Maintenance_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Maintenance maintenance = await _maintenanceRepository.FindAsync(id);
        if (maintenance == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _maintenanceRepository.DeleteAsync(maintenance);
    }

    [Authorize(LimsPermissions.Maintenance_Default)]
    public async Task<MaintenanceDto> GetAsync(Guid id)
    {
        var result = await _maintenanceRepository.FindAsync(id);
        return ObjectMapper.Map<Maintenance, MaintenanceDto>(result);
    }


    [Authorize(LimsPermissions.Maintenance_Default)]
    public async Task<PagedResultDto<MaintenanceDto>> GetPagedListAsync(MaintenanceGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _maintenanceRepository.WithDetailsAsync();

        query = query
          .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.EquipmentId != null, x => x.EquipmentId == input.EquipmentId)
            .WhereIf(input.DateStart != null, x => x.Date >= input.DateStart)
            .WhereIf(input.DateEnd != null, x => x.Date <= input.DateEnd)
            .WhereIf(input.MaintenanceType != null, x => x.MaintenanceType == input.MaintenanceType)
            .WhereIf(input.Result != null, x => x.Result == input.Result)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<MaintenanceDto>(totalCount, ObjectMapper.Map<List<Maintenance>, List<MaintenanceDto>>(result));
    }

    [Authorize(LimsPermissions.Maintenance_Update)]
    public async Task UpdateAsync(Guid id, MaintenanceUpdateDto input)
    {
        Maintenance maintenance = await _maintenanceRepository.FindAsync(id);
        if (maintenance == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        maintenance.EquipmentId = input.EquipmentId;
        maintenance.Date = input.Date;
        maintenance.MaintenanceType = input.MaintenanceType;
        maintenance.Description = input.Description;
        maintenance.Person = input.Person;
        maintenance.SpentTime = input.SpentTime;
        maintenance.Cost = input.Cost;
        maintenance.Result = input.Result;
        maintenance.WorkOrderNumber = input.WorkOrderNumber;
        maintenance.Department = input.Department;
        maintenance.Remark = input.Remark;

        var result = await _maintenanceRepository.UpdateAsync(maintenance);
    }
}
