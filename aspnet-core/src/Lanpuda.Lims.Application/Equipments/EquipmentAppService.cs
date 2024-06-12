using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Equipments.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Lanpuda.Lims.Calibrations;
using Lanpuda.Lims.Maintenances;
using Lanpuda.Lims.Repairs;
using Lanpuda.Lims.UsageHistories;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Equipments;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class EquipmentAppService : LimsAppService, IEquipmentAppService
{

    private readonly IEquipmentRepository _equipmentRepository;
    private readonly ICalibrationRepository _calibrationRepository;
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly IRepairRepository _repairRepository;
    private readonly IUsageHistoryRepository _usageHistoryRepository;

    public EquipmentAppService(
        IEquipmentRepository repository , 
        ICalibrationRepository calibrationRepository,
        IMaintenanceRepository maintenanceRepository,
        IRepairRepository repairRepository,
        IUsageHistoryRepository usageHistoryRepository
        )
    {
        _equipmentRepository = repository;
        _maintenanceRepository = maintenanceRepository;
        _repairRepository = repairRepository;
        _usageHistoryRepository = usageHistoryRepository;        
        _calibrationRepository = calibrationRepository;
    }

    [Authorize(LimsPermissions.Equipment_Create)]
    public async Task CreateAsync(EquipmentCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        //new Equipment and pass input to it
        var equipment = ObjectMapper.Map<EquipmentCreateDto, Equipment>(input);
        await _equipmentRepository.InsertAsync(equipment);
    }


    [Authorize(LimsPermissions.Equipment_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Equipment equipment = await _equipmentRepository.FindAsync(id);
        if (equipment == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //判断是否存在校准记录
        var hasCalibration = await _calibrationRepository.AnyAsync(m=>m.EquipmentId  == id);
        if (hasCalibration)
        {
            throw new UserFriendlyException("存在对应的校准记录,请先删除校准记录");
        }

        //判断是否存在保养记录
        var hasMaintenance = await _maintenanceRepository.AnyAsync(m=>m.EquipmentId == id);
        if (hasMaintenance)
        {
            throw new UserFriendlyException("存在对应的保养记录,请先删除保养记录");
        }

        //判断是否存在维修记录
        var hasRepair = await _repairRepository.AnyAsync(m=>m.EquipmentId == id);
        if (hasRepair)
        {
            throw new UserFriendlyException("存在对应的维修记录,请先删除维修记录");
        }

        //判断是否存在使用记录
        var hasUsageHistory = await _usageHistoryRepository.AnyAsync(m=>m.EquipmentId ==id);
        if (hasUsageHistory)
        {
            throw new UserFriendlyException("存在对应的使用记录,请先删除使用记录");
        }
        await _equipmentRepository.DeleteAsync(equipment);
    }



    [Authorize(LimsPermissions.Equipment_Default)]

    public async Task<EquipmentDto> GetAsync(Guid id)
    {
        var result = await _equipmentRepository.FindAsync(id);
        return ObjectMapper.Map<Equipment, EquipmentDto>(result);
    }



    [Authorize(LimsPermissions.Equipment_Default)]
    public async Task<PagedResultDto<EquipmentDto>> GetPagedListAsync(EquipmentGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _equipmentRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(input.DicEquipmentTypeId != null, x => x.DicEquipmentTypeId == input.DicEquipmentTypeId)
            .WhereIf(!input.Spec.IsNullOrWhiteSpace(), x => x.Spec.Contains(input.Spec))
            .WhereIf(!input.Manufacturer.IsNullOrWhiteSpace(), x => x.Manufacturer.Contains(input.Manufacturer))
            .WhereIf(input.AcquisitionDate != null, x => x.AcquisitionDate == input.AcquisitionDate)
            .WhereIf(!input.OperationManual.IsNullOrWhiteSpace(), x => x.OperationManual.Contains(input.OperationManual))
            .WhereIf(!input.InstallationLocation.IsNullOrWhiteSpace(), x => x.InstallationLocation.Contains(input.InstallationLocation))
            .WhereIf(input.Status != null, x => x.Status == input.Status)
            .WhereIf(!input.CalibrationStandard.IsNullOrWhiteSpace(), x => x.CalibrationStandard.Contains(input.CalibrationStandard))
            .WhereIf(input.MaintenancePeriod != null, x => x.MaintenancePeriod == input.MaintenancePeriod)
            .WhereIf(!input.MaintenanceStandard.IsNullOrWhiteSpace(), x => x.MaintenanceStandard.Contains(input.MaintenanceStandard))
            .WhereIf(!input.Remark.IsNullOrWhiteSpace(), x => x.Remark.Contains(input.Remark))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<EquipmentDto>(totalCount, ObjectMapper.Map<List<Equipment>, List<EquipmentDto>>(result));
    }

    [Authorize(LimsPermissions.Equipment_Update)]
    public async Task UpdateAsync(Guid id, EquipmentUpdateDto input)
    {
        Equipment equipment = await _equipmentRepository.FindAsync(id);
        if (equipment == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        equipment.Name = input.Name;
        equipment.Status = input.Status;
        equipment.MaintenancePeriod = input.MaintenancePeriod;
        equipment.Number = input.Number;
        equipment.DicEquipmentTypeId = input.DicEquipmentTypeId;
        equipment.Spec = input.Spec;
        equipment.Manufacturer = input.Manufacturer;
        equipment.AcquisitionDate = input.AcquisitionDate;
        equipment.OperationManual = input.OperationManual;
        equipment.InstallationLocation = input.InstallationLocation;
        equipment.CalibrationStandard = input.CalibrationStandard;
        equipment.MaintenanceStandard = input.MaintenanceStandard;
        equipment.Remark = input.Remark;

        var result = await _equipmentRepository.UpdateAsync(equipment);
    }
}
