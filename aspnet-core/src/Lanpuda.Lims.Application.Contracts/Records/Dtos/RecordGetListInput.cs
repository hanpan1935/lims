using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Records.Dtos;

[Serializable]
public class RecordGetListInput : PagedAndSortedResultRequestDto
{
    public string? Number { get; set; }

    public string? SampleNumber { get; set;  }

    public Guid? ProductId { get; set; }

    public int? DicSampleTypeId { get; set; }

    public int? DicSamplePropertyId { get; set; }

    public int? DicRatingTypeId { get; set; }

    public DateTime? SampleTimeStart { get; set; }

    public DateTime? SampleTimeEnd { get; set; }

    public string? Sender { get; set; }

    public Guid? CustomerId { get; set; }

    public Guid? SupplierId { get; set; }

}