using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Customers.Dtos
{
    public class CustomerLookupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Number { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShortName { get; set; }
    }
}
