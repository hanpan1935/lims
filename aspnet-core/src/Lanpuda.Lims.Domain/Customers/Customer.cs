using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.Lims.Customers
{
    public class Customer : LimsAuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        public string FullName { get; set; }


        public string ShortName { get; set; }

       
        [MaxLength(256)]
        public string? Manager { get; set; }

        public string? ManagerTel { get; set; }

        [MaxLength(256)]
        public string? Remark { get; set; }


        [MaxLength(256)]
        public string? Consignee { get; set; }

        [MaxLength(256)]
        public string? ConsigneeTel { get; set; }

        [MaxLength(256)]
        public string? Address { get; set; }


       

        protected Customer()
        {
            this.Number = "";
            FullName = "";
            ShortName = "";
        }

        public Customer(
            Guid id,
            string number,
            string fullName,
            string shortName
        ) : base(id)
        {
            Number = number;
            FullName = fullName;
            ShortName = shortName;
        }
    }
}
