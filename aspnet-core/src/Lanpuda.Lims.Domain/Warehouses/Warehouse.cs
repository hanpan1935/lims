using Lanpuda.Lims.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Warehouses
{
    public class Warehouse : LimsAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string? Remark { get; set; }
        public virtual List<Location> Locations { get; set; }

        protected Warehouse()
        {
            this.Name = string.Empty;
            Locations = new List<Location>();
        }

        public Warehouse(
            Guid id,
            string name
        ) : base(id)
        {
            Name = name;
            Locations = new List<Location>();
        }
    }
}
