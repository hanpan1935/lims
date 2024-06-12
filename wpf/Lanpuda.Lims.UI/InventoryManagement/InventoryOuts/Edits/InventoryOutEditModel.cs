using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.InventoryManagement.InventoryOuts.Edits
{
    public class InventoryOutEditModel : ModelBase
    {
        public Guid? Id { get; set; }


        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string? Reason
        {
            get { return GetProperty(() => Reason); }
            set { SetProperty(() => Reason, value); }
        }


        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }


        public InventoryOutDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }


        public ObservableCollection<InventoryOutDetailEditModel> Details { get; set; }

        public InventoryOutEditModel()
        {
            Details = new ObservableCollection<InventoryOutDetailEditModel>();
        }   
    }


    public class InventoryOutDetailEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public Guid InventoryId { get; set; }
        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }


        [MaxLength(128)]
        public string LotNumber
        {
            get { return GetProperty(() => LotNumber); }
            set { SetProperty(() => LotNumber, value); }
        }

        /// <summary>
        /// 出库数量
        /// </summary>
        [Required]
        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }

        public string ProductName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProductNumber
        {
            get { return GetProperty(() => ProductNumber); }
            set { SetProperty(() => ProductNumber, value); }
        }

        public string ProductSpec
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public string ProductUnitName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public string WarehouseName
        {
            get { return GetProperty(() => WarehouseName); }
            set { SetProperty(() => WarehouseName, value); }
        }

        public string LocationName
        {
            get { return GetProperty(() => LocationName); }
            set { SetProperty(() => LocationName, value); }
        }
    }
}
