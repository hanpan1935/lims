using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.Lims.DataDictionaries;

namespace Lanpuda.Lims.InventoryOuts
{
    public class InventoryOut : LimsAuditedAggregateRoot<Guid>
    {
        [Required]
        [MaxLength(128)]
        [Display(Name = "出库单号")]
        public string Number { get; set; }

        public string? Reason { get; set; }

        /// <summary>
        /// 入库状态  false待入库  true已入库
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime? SuccessfulTime { get; set; }


        public string? Remark { get; set; }

        public List<InventoryOutDetail> Details { get; set; }


        protected InventoryOut()
        {
            this.Number = string.Empty;
            Details = new List<InventoryOutDetail>();
        }

        public InventoryOut(Guid id,string number) : base(id)
        {
            this.Number = number;
            Details = new List<InventoryOutDetail>();
        }
    }
}
