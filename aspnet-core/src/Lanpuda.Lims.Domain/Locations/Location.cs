using Lanpuda.Lims.Warehouses;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Locations
{
    //库位
    public class Location : LimsAuditedAggregateRoot<Guid>
    {
        public Guid WarehouseId { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
        public string Name { get; set; }
        public string? Remark { get; set; }

        protected Location()
        {
            this.Name = string.Empty;
        }

        public Location(
            Guid id,
            string name
            
        ) : base(id)
        {
            Name = name;
        }
    }
}
