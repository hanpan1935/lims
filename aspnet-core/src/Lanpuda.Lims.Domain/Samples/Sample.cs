using Lanpuda.Lims.Customers;
using Lanpuda.Lims.DataDictionaries;
using Lanpuda.Lims.Products;
using Lanpuda.Lims.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Samples
{
    public class Sample : LimsAuditedAggregateRoot<Guid>
    {
        //样品编号
        public string Number { get; set; }
        //样品名称
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }


        //样品类型
        public int? DicSampleTypeId { get; set; }
        public DicSampleType? DicSampleType { get; set; }

        //样品属性
        public int? DicSamplePropertyId { get; set; }
        public DicSampleProperty? DicSampleProperty { get; set; }

        //来样时间
        public DateTime SampleTime { get; set; }

        //样品数量
        public double? SampleCount { get; set; }

        //过期日期
        public DateTime? ExpireTime { get; set; }


        //送样人
        public string? Sender { get; set; }

        public string? Remark { get; set; }


        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }


        public Guid? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }


        protected Sample()
        {
            this.Number = string.Empty;
        }

        public Sample(
            Guid id,
            string number
           
        ) : base(id)
        {
            Number = number;
        }
    }
}
