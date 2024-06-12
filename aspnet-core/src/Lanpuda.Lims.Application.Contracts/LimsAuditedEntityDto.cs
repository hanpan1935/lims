using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims
{
    public class LimsAuditedEntityDto<T> : AuditedEntityDto<T>
    {
        public string? CreatorSurname { get; set; }
        public string? LastModifierSurname { get; set; }

        public string? CreatorName { get; set; }
        public string? LastModifierName { get; set; }
        
    }
}
