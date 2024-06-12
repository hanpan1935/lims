using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.InspectionTasks.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class InspectionTaskDto : LimsAuditedEntityDto<Guid>
{
    public Guid RecordId { get; set; }
    public string? RecordNumber { get; set; }
    public string? SampleId { get; set; }
    public string? SampleNumber { get; set; }
    public Guid InspectionItemId { get; set; }
    public string? InspectionItemFullName { get; set; }
    public string? InspectionItemShortName { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public double? ResultValue { get; set; }
    public bool? IsQualified { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public Guid RecordDetailId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public DateTime InspectionDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid EquipmentId { get; set; }

    public string? EquipmentName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Inspector { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }



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
}