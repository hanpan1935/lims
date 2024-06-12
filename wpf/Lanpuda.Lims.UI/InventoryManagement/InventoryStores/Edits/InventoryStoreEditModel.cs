using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.InventoryManagement.InventoryStores.Edits
{
    public  class InventoryStoreEditModel : ModelBase
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


        public InventoryStoreDetailEditModel? SelectedDetailRow
        {
            get { return GetProperty(() => SelectedDetailRow); }
            set { SetProperty(() => SelectedDetailRow, value); }
        }

        public ObservableCollection<InventoryStoreDetailEditModel> Details { get; set; }

        public InventoryStoreEditModel()
        {
            Details = new ObservableCollection<InventoryStoreDetailEditModel>();
        }

    }


    public class InventoryStoreDetailEditModel : ModelBase
    {
        public Guid? Id { get; set; }
        public Guid ProductId { get; set; }

        public Guid LocationId
        {
            get { return GetProperty(() => LocationId); }
            set { SetProperty(() => LocationId, value); }
        }

        [MaxLength(128)]
        public string? LotNumber
        {
            get { return GetProperty(() => LotNumber); }
            set { SetProperty(() => LotNumber, value); }
        }

        /// <summary>
        /// 入库数量
        /// </summary>
        [Required]
        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }


        /// <summary>
        /// /////////////////////////
        /// </summary>
        public string ProductName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    


        public string ProductUnit
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public WarehouseLookupDto? SelectedWarehouse
        {
            get { return GetProperty(() => SelectedWarehouse); }
            set { SetProperty(() => SelectedWarehouse, value, OnSelectedWarehouseChanged); }
        }

        public ObservableCollection<WarehouseLookupDto> WarehouseSource { get; set; }


        public InventoryStoreDetailEditModel()
        {
            WarehouseSource = new ObservableCollection<WarehouseLookupDto>();
        }


        private void OnSelectedWarehouseChanged()
        {
            if (SelectedWarehouse != null)
            {
                var locaiton = SelectedWarehouse.Locations.FirstOrDefault();
                if (locaiton != null)
                {
                    this.LocationId = locaiton.Id;
                }
            }
            ;
        }
    }
}
