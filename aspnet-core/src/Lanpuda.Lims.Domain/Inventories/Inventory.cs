using Lanpuda.Lims.Locations;
using Lanpuda.Lims.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Inventories
{
    public class Inventory : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 库位Id
        /// </summary>
        public Guid LocationId { get; set; }
        public Location? Location { get; set; }


        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }


        public double Quantity { get; set; }


        public string? LotNumber { get; set; }

        ///// <summary>
        ///// 单位价值
        ///// </summary>
        ///// 
        //[Column(TypeName = "decimal(18,2)")]
        //public decimal? Price { get; set; }


        protected Inventory()
        {
        }

        public Inventory(Guid id) : base(id)
        {
        }
    }
}
