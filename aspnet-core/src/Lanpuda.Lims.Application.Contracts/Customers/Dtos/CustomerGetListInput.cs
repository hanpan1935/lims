using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Customers.Dtos;

[Serializable]
public class CustomerGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [DisplayName("CustomerNumber")]
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerFullName")]
    public string? FullName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerShortName")]
    public string? ShortName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerManager")]
    public string? Manager { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerManagerTel")]
    public string? ManagerTel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerRemark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerConsignee")]
    public string? Consignee { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerConsigneeTel")]
    public string? ConsigneeTel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CustomerAddress")]
    public string? Address { get; set; }

    public CustomerGetListInput()
    {
        this.Number = string.Empty;
        this.FullName = string.Empty;
        this.ShortName = string.Empty;
        this.Manager = string.Empty;
        this.ManagerTel = string.Empty;
        this.Remark = string.Empty;
        this.Consignee = string.Empty;
        this.ConsigneeTel = string.Empty;
        this.Address = string.Empty;
    }
}