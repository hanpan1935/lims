using AutoMapper;
using Lanpuda.Lims.Calibrations.Dtos;
using Lanpuda.Lims.Customers.Dtos;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Lims.InspectionItems.Dtos;
using Lanpuda.Lims.InspectionTasks.Dtos;
using Lanpuda.Lims.Locations.Dtos;
using Lanpuda.Lims.Maintenances.Dtos;
using Lanpuda.Lims.Products.Dtos;
using Lanpuda.Lims.Records.Dtos;
using Lanpuda.Lims.Repairs.Dtos;
using Lanpuda.Lims.Samples.Dtos;
using Lanpuda.Lims.Standards.Dtos;
using Lanpuda.Lims.Suppliers.Dtos;
using Lanpuda.Lims.UI.BasicInformations.Customers.Edits;
using Lanpuda.Lims.UI.BasicInformations.Locations.Edits;
using Lanpuda.Lims.UI.BasicInformations.Products.Edits;
using Lanpuda.Lims.UI.BasicInformations.Suppliers.Edits;
using Lanpuda.Lims.UI.BasicInformations.Warehouses.Edits;
using Lanpuda.Lims.UI.EquipmentManagement.Calibrations.Edits;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Edits;
using Lanpuda.Lims.UI.EquipmentManagement.Maintenances.Edits;
using Lanpuda.Lims.UI.EquipmentManagement.Repairs.Edits;
using Lanpuda.Lims.UI.EquipmentManagement.UsageHistories.Edits;
using Lanpuda.Lims.UI.InspectionMethods.InspectionItems.Edits;
using Lanpuda.Lims.UI.InspectionMethods.Standards.Edits;
using Lanpuda.Lims.UI.InspectionTasks.Dashboards;
using Lanpuda.Lims.UI.InspectionTasks.Edits;
using Lanpuda.Lims.UI.Records.Edits;
using Lanpuda.Lims.UI.Samples.Edits;
using Lanpuda.Lims.UsageHistories.Dtos;
using Lanpuda.Lims.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AutoMapper;
using static Lanpuda.Lims.Permissions.LimsPermissions;

namespace Lanpuda.Lims.UI
{
    public class LimsUIProfile : Profile
    {
        //CreateMap<ArrivalNoticeDto, ArrivalNoticePagedModel>();
        //CreateMap<PurchaseReturnApplyDto, PurchaseReturnApplyPagedModel>();
        public LimsUIProfile()
        {
            CreateMap<CustomerDto, CustomerEditModel>();
            CreateMap<CustomerEditModel, CustomerUpdateDto>();
            CreateMap<CustomerEditModel, CustomerCreateDto>();

            CreateMap<ProductDto, ProductEditModel>();
            CreateMap<ProductEditModel, ProductCreateDto>();
            CreateMap<ProductEditModel, ProductUpdateDto>();


            CreateMap<SupplierDto, SupplierEditModel>();
            CreateMap<SupplierEditModel, SupplierCreateDto>();
            CreateMap<SupplierEditModel, SupplierUpdateDto>();


            CreateMap<WarehouseDto, WarehouseEditModel>();
            CreateMap<WarehouseEditModel, WarehouseCreateDto>();
            CreateMap<WarehouseEditModel, WarehouseUpdateDto>();


            CreateMap<LocationDto, LocationEditModel>();
            CreateMap<LocationEditModel, LocationCreateDto>();
            CreateMap<LocationEditModel, LocationUpdateDto>();


            CreateMap<SampleDto, SampleEditModel>();
            CreateMap<SampleEditModel, SampleCreateDto>();
            CreateMap<SampleEditModel, SampleUpdateDto>();

            CreateMap<EquipmentDto, EquipmentEditModel>();
            CreateMap<EquipmentEditModel, EquipmentCreateDto>();
            CreateMap<EquipmentEditModel, EquipmentUpdateDto>();

            CreateMap<CalibrationDto, CalibrationEditModel>();
            CreateMap<CalibrationEditModel, CalibrationCreateDto>();
            CreateMap<CalibrationEditModel, CalibrationUpdateDto>();


            CreateMap<MaintenanceDto, MaintenanceEditModel>();
            CreateMap<MaintenanceEditModel, MaintenanceCreateDto>();
            CreateMap<MaintenanceEditModel, MaintenanceUpdateDto>();


            CreateMap<RepairDto, RepairEditModel>();
            CreateMap<RepairEditModel, RepairCreateDto>();
            CreateMap<RepairEditModel, RepairUpdateDto>();


            CreateMap<UsageHistoryDto, UsageHistoryEditModel>();
            CreateMap<UsageHistoryEditModel, UsageHistoryCreateDto>();
            CreateMap<UsageHistoryEditModel, UsageHistoryUpdateDto>();


            CreateMap<InspectionItemDto, InspectionItemEditModel>();
            CreateMap<InspectionItemEditModel, InspectionItemCreateDto>();
            CreateMap<InspectionItemEditModel, InspectionItemUpdateDto>();



            CreateMap<StandardDto, StandardEditModel>();
            CreateMap<StandardEditModel, StandardCreateDto>();
            CreateMap<StandardEditModel, StandardUpdateDto>();

            CreateMap<StandardDetailDto, StandardDetailEditModel>();
            CreateMap<StandardDetailEditModel, StandardDetailCreateDto>();
            CreateMap<StandardDetailEditModel, StandardDetailUpdateDto>();


            CreateMap<RecordDto,      RecordEditModel>();
            CreateMap<RecordEditModel,RecordCreateDto>();
            CreateMap<RecordEditModel, RecordUpdateDto>();

            CreateMap<RecordDetailDto, RecordDetailEditModel>();
            CreateMap<RecordDetailEditModel, RecordDetailCreateDto>();
            CreateMap<RecordDetailEditModel, RecordDetailUpdateDto>();


            CreateMap<InspectionTaskDto, InspectionTaskEditModel>();
            CreateMap<InspectionTaskDto, InspectionTaskDetailModel>();
            CreateMap<InspectionTaskEditModel, InspectionTaskCreateDto>();
            CreateMap<InspectionTaskEditModel, InspectionTaskUpdateDto>();
            
        }
    }
}
