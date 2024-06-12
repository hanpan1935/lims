using Lanpuda.Lims.InspectionItems.Dtos;
using Lanpuda.Lims.InspectionTasks.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Records.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class RecordDetailDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public Guid InspectionItemId { get; set; }

    public string? InspectionItemFullName { get; set; }

    public string? InspectionItemShortName { get; set; }

    public Guid? DefaultEquipmentId { get; set; }
    public string? DefaultEquipmentName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double? MinValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool HasMinValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double? MaxValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool HasMaxValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double? ResultValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool? IsQualified { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<InspectionTaskDto> InspectionTaskList { get; set; }


    private string? standard;
    public string? Standard
    {
        get
        {
            string? value = string.Empty;
            if (this.MinValue != null)
            {
                if (this.HasMinValue == true)
                {
                    value += this.MinValue + "¡Ü";
                }
                else
                {
                    value += this.MinValue + "<";
                }
            }

            if (this.MaxValue != null)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value += "&";
                }

                if (this.HasMaxValue == true)
                {
                    value += "¡Ü" + this.MaxValue;
                }
                else
                {
                    value += "<" + this.MaxValue;
                }
            }



            return value;
        }
        set => standard = value;
    }

    public RecordDetailDto()
    {
        InspectionTaskList = new List<InspectionTaskDto>();
    }
}