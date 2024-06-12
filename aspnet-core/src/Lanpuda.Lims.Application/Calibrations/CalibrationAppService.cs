using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Calibrations.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Volo.Abp.ObjectMapping;
using Lanpuda.UniqueCode;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Calibrations;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class CalibrationAppService : LimsAppService, ICalibrationAppService
{

    private readonly ICalibrationRepository _calibrationRepository;
    private readonly IUniqueCodeGenerator _uniqueCodeGenerator;

    public CalibrationAppService(ICalibrationRepository repository, IUniqueCodeGenerator uniqueCodeGenerator)
    {
        _calibrationRepository = repository;
        _uniqueCodeGenerator = uniqueCodeGenerator; 
    }


    [Authorize(LimsPermissions.Calibration_Create)]
    public async Task CreateAsync(CalibrationCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeGenerator.GetUniqueNumberAsync(LimsNumberPrefix.CalibrationPrefix);
        //new Calibration and pass input to it
        var calibration = new Calibration(id);
        calibration.Number = number;
        calibration.EquipmentId = input.EquipmentId;
        calibration.CalibrationDate = input.CalibrationDate;
        calibration.NextCalibrationDate = input.NextCalibrationDate;
        calibration.CalibrationResult = input.CalibrationResult;
        calibration.Person = input.Person;
        calibration.CertificateNumber = input.CertificateNumber;
        calibration.Cost = input.Cost;
        calibration.Standard = input.Standard;
        calibration.Remark = input.Remark;
        await _calibrationRepository.InsertAsync(calibration);
    }


    [Authorize(LimsPermissions.Calibration_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Calibration calibration = await _calibrationRepository.FindAsync(id);
        if (calibration == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _calibrationRepository.DeleteAsync(calibration);
    }


    [Authorize(LimsPermissions.Calibration_Default)]
    public async Task<CalibrationDto> GetAsync(Guid id)
    {
        var result = await _calibrationRepository.FindAsync(id);
        return ObjectMapper.Map<Calibration, CalibrationDto>(result);
    }


    [Authorize(LimsPermissions.Calibration_Default)]
    public async Task<PagedResultDto<CalibrationDto>> GetPagedListAsync(CalibrationGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _calibrationRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.EquipmentId != null, x => x.EquipmentId == input.EquipmentId)
            .WhereIf(input.CalibrationDate != null, x => x.CalibrationDate == input.CalibrationDate)
            .WhereIf(input.NextCalibrationDate != null, x => x.NextCalibrationDate == input.NextCalibrationDate)
            .WhereIf(input.CalibrationResult != null, x => x.CalibrationResult == input.CalibrationResult)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<CalibrationDto>(totalCount, ObjectMapper.Map<List<Calibration>, List<CalibrationDto>>(result));
    }

    [Authorize(LimsPermissions.Calibration_Update)]
    public async Task UpdateAsync(Guid id, CalibrationUpdateDto input)
    {
        Calibration calibration = await _calibrationRepository.FindAsync(id);
        if (calibration == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        calibration.EquipmentId = input.EquipmentId;
        calibration.CalibrationDate = input.CalibrationDate;
        calibration.NextCalibrationDate = input.NextCalibrationDate;
        calibration.CalibrationResult = input.CalibrationResult;
        calibration.Person = input.Person;
        calibration.CertificateNumber = input.CertificateNumber;
        calibration.Cost = input.Cost;
        calibration.Standard = input.Standard;
        calibration.Remark = input.Remark;
        var result = await _calibrationRepository.UpdateAsync(calibration);
    }
}
