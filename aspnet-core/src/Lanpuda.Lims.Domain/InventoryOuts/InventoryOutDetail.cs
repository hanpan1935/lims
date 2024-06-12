using Lanpuda.Lims.Locations;
using Lanpuda.Lims.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.InventoryOuts
{
    public class InventoryOutDetail : LimsAuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid InventoryOutId { get; set; }
        public InventoryOut InventoryOut { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        [MaxLength(128)]
        public string? LotNumber { get; set; }

        /// <summary>
        /// 出库数量
        /// </summary>
        [Required]
        public double Quantity { get; set; }


        public int Sort { get; set; }

        protected InventoryOutDetail()
        {
        }

        public InventoryOutDetail(Guid id ) : base(id)
        {
            
        }
    }
}
