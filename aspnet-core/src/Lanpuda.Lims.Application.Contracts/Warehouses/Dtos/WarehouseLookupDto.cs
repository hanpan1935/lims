using Lanpuda.Lims.Locations.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Warehouses.Dtos
{
    public class WarehouseLookupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }


        public List<LocationDto> Locations { get; set; }


        public WarehouseLookupDto()
        {
            Locations = new List<LocationDto>();
            this.Name = string.Empty;
            this.Remark = string.Empty;
            Locations = new List<LocationDto>();
        }
    }
}
