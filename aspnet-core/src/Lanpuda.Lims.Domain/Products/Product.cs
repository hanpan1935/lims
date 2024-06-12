using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.Lims.Standards;
using Lanpuda.Lims.DataDictionaries;

namespace Lanpuda.Lims.Products
{
    public class Product : LimsAuditedAggregateRoot<Guid>
    {
        [Required]
        public string Name { get; set; }


        [Required]
        public string Unit { get; set; }


        [MaxLength(256)]
        public string? Number { get; set; }


        public int? DicProductTypeId { get; set; }
        [ForeignKey(nameof(DicProductTypeId))]
        public DicProductType? DicProductType { get; set; }


        [MaxLength(256)]
        public string? Spec { get; set; }

        /// <summary>
        /// 默认检验标准
        /// </summary>
        public Guid? StandardId { get; set; }   
        public Standard? Standard { get; set; }


        public string? Remark { get; set; }

        protected Product()
        {
            this.Name = string.Empty;
            this.Unit = string.Empty;
        }

        public Product(
            Guid id,
            string name,
            string unit
        ) : base(id)
        {
            Name = name;
            Unit = unit;
        }
    }
}
