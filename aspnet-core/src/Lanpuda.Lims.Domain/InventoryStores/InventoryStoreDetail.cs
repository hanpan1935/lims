using Lanpuda.Lims.InventoryOuts;
using Lanpuda.Lims.Locations;
using Lanpuda.Lims.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.InventoryStores
{
    public class InventoryStoreDetail : LimsAuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid InventoryStoreId { get; set; }
        public InventoryStore InventoryStore { get; set; }


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

        protected InventoryStoreDetail()
        {
        }

        public InventoryStoreDetail(Guid id) : base(id)
        {
            
        }
    }
}
