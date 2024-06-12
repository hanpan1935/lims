using Lanpuda.Lims.Locations;
using Lanpuda.Lims.Products;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.InventoryLogs
{
    /// <summary>
    /// 库存流水
    /// </summary>
    public class InventoryLog : LimsAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 发生单号
        /// </summary>
        public string Number { get; set; }


        public Guid ProductId { get; set; }
        public Product? Product { get; set; }


        public Guid LocationId { get; set; }
        public Location? Location { get; set; }


        /// <summary>
        ///  出入时间
        /// </summary>
        public DateTime LogTime { get; set; }


        /// <summary>
        /// 出入库原因
        /// </summary>
        public string? Reason { get; set; }


        /// <summary>
        /// 批次号
        /// </summary>
        public string? LotNumber { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        public double InQuantity { get; set; }


        /// <summary>
        /// 出库数量
        /// </summary>
        public double OutQuantity { get; set; }


        /// <summary>
        /// 发生后数量
        /// </summary>
        public double AfterQuantity { get; set; }


        protected InventoryLog()
        {
            this.Number = string.Empty;
            this.LotNumber = string.Empty;
        }

        public InventoryLog(Guid id) : base(id)
        {
        }
    }
}
