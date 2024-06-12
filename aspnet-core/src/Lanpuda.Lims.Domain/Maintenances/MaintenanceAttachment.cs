using Lanpuda.Lims.Calibrations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Maintenances
{
    public class MaintenanceAttachment : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string FileExtension { get; set; }

        public Guid MaintenanceId { get; set; }
        public virtual Maintenance Maintenance { get; set; }
    }
}
