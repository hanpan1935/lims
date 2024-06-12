using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Equipments
{
    public class EquipmentAttachment : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string FileExtension { get; set; }

        public Guid EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
