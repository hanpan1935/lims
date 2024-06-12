using System;
using System.ComponentModel;

namespace Lanpuda.Lims.Customers.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class CustomerUpdateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerNumber")]
    public string Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerFullName")]
    public string FullName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerShortName")]
    public string ShortName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerManager")]
    public string Manager { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerManagerTel")]
    public string ManagerTel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerRemark")]
    public string Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerConsignee")]
    public string Consignee { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerConsigneeTel")]
    public string ConsigneeTel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerAddress")]
    public string Address { get; set; }
}