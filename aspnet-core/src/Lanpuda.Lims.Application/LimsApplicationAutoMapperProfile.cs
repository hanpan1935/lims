using Lanpuda.Lims.Calibrations;
using Lanpuda.Lims.Calibrations.Dtos;
using Lanpuda.Lims.Customers;
using Lanpuda.Lims.Customers.Dtos;
using Lanpuda.Lims.DataDictionaries;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.Equipments;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Lims.InspectionItems;
using Lanpuda.Lims.InspectionItems.Dtos;
using Lanpuda.Lims.InspectionTasks;
using Lanpuda.Lims.InspectionTasks.Dtos;
using Lanpuda.Lims.Inventories;
using Lanpuda.Lims.Inventories.Dtos;
using Lanpuda.Lims.InventoryLogs;
using Lanpuda.Lims.InventoryLogs.Dtos;
using Lanpuda.Lims.InventoryOuts;
using Lanpuda.Lims.InventoryOuts.Dtos;
using Lanpuda.Lims.InventoryStores;
using Lanpuda.Lims.InventoryStores.Dtos;
using Lanpuda.Lims.Locations;
using Lanpuda.Lims.Locations.Dtos;
using Lanpuda.Lims.Maintenances;
using Lanpuda.Lims.Maintenances.Dtos;
using Lanpuda.Lims.Products;
using Lanpuda.Lims.Products.Dtos;
using Lanpuda.Lims.Records;
using Lanpuda.Lims.Records.Dtos;
using Lanpuda.Lims.Repairs;
using Lanpuda.Lims.Repairs.Dtos;
using Lanpuda.Lims.Samples;
using Lanpuda.Lims.Samples.Dtos;
using Lanpuda.Lims.Standards;
using Lanpuda.Lims.Standards.Dtos;
using Lanpuda.Lims.UsageHistories;
using Lanpuda.Lims.UsageHistories.Dtos;
using Lanpuda.Lims.Suppliers;
using Lanpuda.Lims.Suppliers.Dtos;
using Lanpuda.Lims.Warehouses;
using Lanpuda.Lims.Warehouses.Dtos;
using AutoMapper;
using Volo.Abp.AutoMapper;

namespace Lanpuda.Lims;

public class LimsApplicationAutoMapperProfile : Profile
{
    public LimsApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Calibration, CalibrationDto>();
        CreateMap<CalibrationCreateDto, Calibration>(MemberList.Source);
        CreateMap<CalibrationUpdateDto, Calibration>(MemberList.Source);
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerCreateDto, Customer>(MemberList.Source);
        CreateMap<CustomerUpdateDto, Customer>(MemberList.Source);
        CreateMap<Equipment, EquipmentDto>();
        CreateMap<EquipmentCreateDto, Equipment>(MemberList.Source);
        CreateMap<EquipmentUpdateDto, Equipment>(MemberList.Source);
        CreateMap<InspectionItem, InspectionItemDto>();
        CreateMap<InspectionItemCreateDto, InspectionItem>(MemberList.Source);
        CreateMap<InspectionItemUpdateDto, InspectionItem>(MemberList.Source);
       
        CreateMap<InspectionTask, InspectionTaskDto>()
            .ForMember(m => m.RecordId, x => x.MapFrom(m => m.RecordDetail.RecordId))
            .ForMember(m => m.RecordNumber, x => x.MapFrom(m => m.RecordDetail.Record.Number))
            .ForMember(m => m.SampleId, x => x.MapFrom(m => m.RecordDetail.Record.SampleId))
            .ForMember(m => m.SampleNumber, x => x.MapFrom(m => m.RecordDetail.Record.Sample.Number))
            .ForMember(m => m.InspectionItemId, x => x.MapFrom(m => m.RecordDetail.InspectionItemId))
            .ForMember(m => m.InspectionItemFullName, x => x.MapFrom(m => m.RecordDetail.InspectionItem.FullName))
            .ForMember(m => m.InspectionItemShortName, x => x.MapFrom(m => m.RecordDetail.InspectionItem.ShortName))
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.RecordDetail.Record.Sample.ProductId))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.RecordDetail.Record.Sample.Product.Name))
            .ForMember(m => m.ResultValue, x => x.MapFrom(m => m.ResultValue))
            .ForMember(m => m.IsQualified, x => x.MapFrom(m => m.RecordDetail.IsQualified))
            .ForMember(m => m.MinValue, x => x.MapFrom(m => m.RecordDetail.MinValue))
            .ForMember(m => m.HasMinValue, x => x.MapFrom(m => m.RecordDetail.HasMinValue))
            .ForMember(m => m.MaxValue, x => x.MapFrom(m => m.RecordDetail.MaxValue))
            .ForMember(m => m.HasMaxValue, x => x.MapFrom(m => m.RecordDetail.HasMaxValue))
            .Ignore(m => m.Standard)
            ;
        CreateMap<InspectionTaskCreateDto, InspectionTask>(MemberList.Source);
        CreateMap<InspectionTaskUpdateDto, InspectionTask>(MemberList.Source);
        CreateMap<Inventory, InventoryDto>()
             .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name));
       
        CreateMap<InventoryLog, InventoryLogDto>()
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            ;
        CreateMap<InventoryOut, InventoryOutDto>();
        CreateMap<InventoryOutCreateDto, InventoryOut>(MemberList.Source);
        CreateMap<InventoryOutUpdateDto, InventoryOut>(MemberList.Source);
        CreateMap<InventoryOutDetail, InventoryOutDetailDto>()
            .ForMember(m => m.WarehouseId, x => x.MapFrom(m => m.Location.WarehouseId))
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            ;

        CreateMap<InventoryOutDetailCreateDto, InventoryOutDetail>(MemberList.Source);
        CreateMap<InventoryOutDetailUpdateDto, InventoryOutDetail>(MemberList.Source);
        CreateMap<InventoryStore, InventoryStoreDto>()
           
            ;
        CreateMap<InventoryStoreCreateDto, InventoryStore>(MemberList.Source);
        CreateMap<InventoryStoreUpdateDto, InventoryStore>(MemberList.Source);
        CreateMap<InventoryStoreDetail, InventoryStoreDetailDto>()
            .ForMember(m => m.WarehouseId, x => x.MapFrom(m => m.Location.WarehouseId))
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.Product.Name))
            ;
        CreateMap<InventoryStoreDetailCreateDto, InventoryStoreDetail>(MemberList.Source);
        CreateMap<InventoryStoreDetailUpdateDto, InventoryStoreDetail>(MemberList.Source);
        CreateMap<Location, LocationDto>();
        CreateMap<LocationCreateDto, Location>(MemberList.Source);
        CreateMap<LocationUpdateDto, Location>(MemberList.Source);
        CreateMap<Maintenance, MaintenanceDto>();
        CreateMap<MaintenanceCreateDto, Maintenance>(MemberList.Source);
        CreateMap<MaintenanceUpdateDto, Maintenance>(MemberList.Source);
        CreateMap<Product, ProductDto>();
        CreateMap<ProductCreateDto, Product>(MemberList.Source);
        CreateMap<ProductUpdateDto, Product>(MemberList.Source);
        CreateMap<Record, RecordDto>()
           
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.Sample.Product.Name))
            .ForMember(m => m.DicSampleTypeId, x => x.MapFrom(m => m.Sample.DicSampleTypeId))
            .ForMember(m => m.DicSampleTypeDisplayValue, x => x.MapFrom(m => m.Sample.DicSampleType.DisplayValue))
            .ForMember(m => m.DicSamplePropertyId, x => x.MapFrom(m => m.Sample.DicSamplePropertyId))
            .ForMember(m => m.DicSamplePropertyDisplayValue, x => x.MapFrom(m => m.Sample.DicSampleProperty.DisplayValue))
            .ForMember(m => m.SampleTime, x => x.MapFrom(m => m.Sample.SampleTime))
            .ForMember(m => m.ExpireTime, x => x.MapFrom(m => m.Sample.ExpireTime))
            .ForMember(m => m.SampleCount, x => x.MapFrom(m => m.Sample.SampleCount))
            .ForMember(m => m.Sender, x => x.MapFrom(m => m.Sample.Sender))
            .ForMember(m => m.CustomerId, x => x.MapFrom(m => m.Sample.CustomerId))
            .ForMember(m => m.CustomerFullName, x => x.MapFrom(m => m.Sample.Customer.FullName))
            .ForMember(m => m.CustomerShortName, x => x.MapFrom(m => m.Sample.Customer.ShortName))
            .ForMember(m => m.SupplierId, x => x.MapFrom(m => m.Sample.SupplierId))
            .ForMember(m => m.SupplierFullName, x => x.MapFrom(m => m.Sample.Supplier.FullName))
            .ForMember(m => m.SupplierShortName, x => x.MapFrom(m => m.Sample.Supplier.ShortName))
            ;
        CreateMap<RecordCreateDto, Record>(MemberList.Source);
        CreateMap<RecordUpdateDto, Record>(MemberList.Source);
        CreateMap<RecordDetail, RecordDetailDto>()
            .Ignore(m => m.Standard)
            .ForMember(m => m.InspectionItemFullName, x => x.MapFrom(m => m.InspectionItem.FullName))
            .ForMember(m => m.InspectionItemShortName, x => x.MapFrom(m => m.InspectionItem.ShortName))
            .ForMember(m => m.DefaultEquipmentId, x => x.MapFrom(m => m.InspectionItem.DefaultEquipmentId))
            .ForMember(m => m.DefaultEquipmentName, x => x.MapFrom(m => m.InspectionItem.DefaultEquipment.Name))
            ;
        CreateMap<RecordDetailCreateDto, RecordDetail>(MemberList.Source);
        CreateMap<RecordDetailUpdateDto, RecordDetail>(MemberList.Source);
        CreateMap<Repair, RepairDto>();
        CreateMap<RepairCreateDto, Repair>(MemberList.Source);
        CreateMap<RepairUpdateDto, Repair>(MemberList.Source);
        CreateMap<Sample, SampleDto>();
        CreateMap<SampleCreateDto, Sample>(MemberList.Source);
        CreateMap<SampleUpdateDto, Sample>(MemberList.Source);
        CreateMap<Standard, StandardDto>();
        CreateMap<StandardCreateDto, Standard>(MemberList.Source);
        CreateMap<StandardUpdateDto, Standard>(MemberList.Source);
        CreateMap<StandardDetail, StandardDetailDto>();
        CreateMap<StandardDetailCreateDto, StandardDetail>(MemberList.Source);
        CreateMap<StandardDetailUpdateDto, StandardDetail>(MemberList.Source);
        CreateMap<UsageHistory, UsageHistoryDto>();
        CreateMap<UsageHistoryCreateDto, UsageHistory>(MemberList.Source);
        CreateMap<UsageHistoryUpdateDto, UsageHistory>(MemberList.Source);
        CreateMap<Supplier, SupplierDto>();
        CreateMap<SupplierCreateDto, Supplier>(MemberList.Source);
        CreateMap<SupplierUpdateDto, Supplier>(MemberList.Source);
        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<Warehouse, WarehouseLookupDto>();
        CreateMap<WarehouseCreateDto, Warehouse>(MemberList.Source);
        CreateMap<WarehouseUpdateDto, Warehouse>(MemberList.Source);

        //CreateMap<DicEquipmentType, DataDictionaryDto>();
        //CreateMap<DicProductType, DataDictionaryDto>();
        //CreateMap<DicRatingType, DataDictionaryDto>();
        //CreateMap<DicSampleProperty, DataDictionaryDto>();
        //CreateMap<DicSampleType, DataDictionaryDto>();
        //CreateMap<DicStandardType, DataDictionaryDto>();

    }
}
