using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.BasicInformations.Customers.Edits
{
    public class CustomerEditModel : ModelBase
    {
        public Guid? Id { get; set; }


        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string FullName
        {
            get { return GetProperty(() => FullName); }
            set { SetProperty(() => FullName, value); }
        }


        public string ShortName
        {
            get { return GetProperty(() => ShortName); }
            set { SetProperty(() => ShortName, value); }
        }


        [MaxLength(256)]
        public string? Manager
        {
            get { return GetProperty(() => Manager); }
            set { SetProperty(() => Manager, value); }
        }

        public string? ManagerTel
        {
            get { return GetProperty(() => ManagerTel); }
            set { SetProperty(() => ManagerTel, value); }
        }

        [MaxLength(256)]
        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }


        [MaxLength(256)]
        public string? Consignee
        {
            get { return GetProperty(() => Consignee); }
            set { SetProperty(() => Consignee, value); }
        }

        [MaxLength(256)]
        public string? ConsigneeTel
        {
            get { return GetProperty(() => ConsigneeTel); }
            set { SetProperty(() => ConsigneeTel, value); }
        }

        [MaxLength(256)]
        public string? Address
        {
            get { return GetProperty(() => Address); }
            set { SetProperty(() => Address, value); }
        }

    }
}
