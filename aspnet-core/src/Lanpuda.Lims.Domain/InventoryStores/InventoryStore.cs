using Lanpuda.Lims.DataDictionaries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.InventoryStores
{
    public class InventoryStore : LimsAuditedAggregateRoot<Guid>
    {
        [Required]
        [MaxLength(128)]
        [Display(Name = "入库单号")]
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


        public List<InventoryStoreDetail> Details { get; set; }


        protected InventoryStore()
        {
            this.Number = string.Empty;
            this.Details = new List<InventoryStoreDetail>();
        }

        public InventoryStore(
            Guid id,
            string number
        ) : base(id)
        {
            Number = number;
            Details = new List<InventoryStoreDetail>();
        }
    }
}
