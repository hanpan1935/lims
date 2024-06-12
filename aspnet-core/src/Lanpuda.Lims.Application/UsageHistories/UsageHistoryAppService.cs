using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.UsageHistories.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Lanpuda.UniqueCode;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.UsageHistories;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class UsageHistoryAppService : LimsAppService, IUsageHistoryAppService
{
    private readonly IUniqueCodeGenerator _uniqueCodeGenerator;
    private readonly IUsageHistoryRepository _usageHistoryRepository;

    public UsageHistoryAppService(IUsageHistoryRepository repository, IUniqueCodeGenerator uniqueCodeGenerator)
    {
        _usageHistoryRepository = repository;
        _uniqueCodeGenerator = uniqueCodeGenerator;
    }

    [Authorize(LimsPermissions.UsageHistory_Create)]
    public async Task CreateAsync(UsageHistoryCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeGenerator.GetUniqueNumberAsync(LimsNumberPrefix.UsageHistoryPrefix);
        var usageHistory = new UsageHistory(id, number);
        usageHistory.EquipmentId = input.EquipmentId;
        usageHistory.UsageHistoryType = UsageHistoryType.Manual;
        usageHistory.StartTime = input.StartTime;
        usageHistory.EndTime = input.EndTime;
        usageHistory.Person = input.Person;
        usageHistory.Project = input.Project;
        usageHistory.Description = input.Description;
        usageHistory.Remark = input.Remark;
        usageHistory.Department = input.Department;

        await _usageHistoryRepository.InsertAsync(usageHistory);
    }

    [Authorize(LimsPermissions.UsageHistory_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        UsageHistory usageHistory = await _usageHistoryRepository.FindAsync(id);
        if (usageHistory == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _usageHistoryRepository.DeleteAsync(usageHistory);
    }

    [Authorize(LimsPermissions.UsageHistory_Default)]
    public async Task<UsageHistoryDto> GetAsync(Guid id)
    {
        var result = await _usageHistoryRepository.FindAsync(id);
        return ObjectMapper.Map<UsageHistory, UsageHistoryDto>(result);
    }

    [Authorize(LimsPermissions.UsageHistory_Default)]
    public async Task<PagedResultDto<UsageHistoryDto>> GetPagedListAsync(UsageHistoryGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _usageHistoryRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.EquipmentId != null, x => x.EquipmentId == input.EquipmentId)
            .WhereIf(input.UsageHistoryType != null, x => x.UsageHistoryType == input.UsageHistoryType)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<UsageHistoryDto>(totalCount, ObjectMapper.Map<List<UsageHistory>, List<UsageHistoryDto>>(result));
    }

    [Authorize(LimsPermissions.UsageHistory_Update)]
    public async Task UpdateAsync(Guid id, UsageHistoryUpdateDto input)
    {
        UsageHistory usageHistory = await _usageHistoryRepository.FindAsync(id);
        if (usageHistory == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        usageHistory.EquipmentId = input.EquipmentId;
        usageHistory.UsageHistoryType = UsageHistoryType.Manual;
        usageHistory.StartTime = input.StartTime;
        usageHistory.EndTime = input.EndTime;
        usageHistory.Person = input.Person;
        usageHistory.Project = input.Project;
        usageHistory.Description = input.Description;
        usageHistory.Remark = input.Remark;
        usageHistory.Department = input.Department;
        var result = await _usageHistoryRepository.UpdateAsync(usageHistory);
    }
}
