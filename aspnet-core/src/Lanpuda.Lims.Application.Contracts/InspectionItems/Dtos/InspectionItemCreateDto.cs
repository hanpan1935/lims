using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lanpuda.Lims.InspectionItems.Dtos;

[Serializable]
public class InspectionItemCreateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionItemShortName")]
    public string ShortName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionItemFullName")]
    public string FullName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionItemBasis")]
    public string Basis { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionItemUnit")]
    public string Unit { get; set; }



    public Guid? DefaultEquipmentId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionItemRemark")]
    public string? Remark { get; set; }





    public InspectionItemCreateDto()
    {
        this.ShortName = string.Empty;
        this.FullName = string.Empty;
        this.Basis = string.Empty;
        this.Unit = string.Empty;
    }
}