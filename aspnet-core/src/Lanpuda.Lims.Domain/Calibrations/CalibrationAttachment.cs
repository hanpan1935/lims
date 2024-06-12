using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Calibrations
{
    public class CalibrationAttachment : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string FileExtension { get; set; }

        public Guid CalibrationId { get; set; }
        public virtual Calibration Calibration { get; set; }    
    }
}
