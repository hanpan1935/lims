using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.UI.BasicInformations.DataDictionaries;
using Lanpuda.Lims.UI.BasicInformations.Locations;
using Lanpuda.Lims.UI.BasicInformations.Products;
using Lanpuda.Lims.UI.BasicInformations.Suppliers;
using Lanpuda.Lims.UI.BasicInformations.Warehouses;
using Lanpuda.Lims.UI.EquipmentManagement.Calibrations;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments;
using Lanpuda.Lims.UI.EquipmentManagement.Maintenances;
using Lanpuda.Lims.UI.EquipmentManagement.Repairs;
using Lanpuda.Lims.UI.EquipmentManagement.UsageHistories;
using Lanpuda.Lims.UI.Home;
using Lanpuda.Lims.UI.InspectionMethods.InspectionItems;
using Lanpuda.Lims.UI.InspectionMethods.Standards;
using Lanpuda.Lims.UI.InspectionTasks;
using Lanpuda.Lims.UI.InspectionTasks.Dashboards;
using Lanpuda.Lims.UI.InventoryManagement.Inventories;
using Lanpuda.Lims.UI.InventoryManagement.InventoryChecks;
using Lanpuda.Lims.UI.InventoryManagement.InventoryLogs;
using Lanpuda.Lims.UI.InventoryManagement.InventoryOuts;
using Lanpuda.Lims.UI.InventoryManagement.InventoryStores;
using Lanpuda.Lims.UI.InventoryManagement.SafetyStocks;
using Lanpuda.Lims.UI.Records;
using Lanpuda.Lims.UI.Records.Items;
using Lanpuda.Lims.UI.Samples;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI
{
    public class LimsUIBootstraper
    {
        private IModuleManager _uiModuleManager;
        private readonly IServiceProvider _serviceProvider;

        public LimsUIBootstraper(IModuleManager moduleManager, IServiceProvider serviceProvider)
        {
            _uiModuleManager = moduleManager;
            _serviceProvider = serviceProvider;
        }


        public void Load()
        {
            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Home,
            () =>
            {
                var viewModel = _serviceProvider.GetService<HomeViewModel>();
                return viewModel;
            },
            typeof(HomeView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Customer,
            () =>
            {
                var viewModel = _serviceProvider.GetService<CustomerPagedViewModel>();
                return viewModel;
            },
            typeof(CustomerPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Location,
            () =>
            {
                var viewModel = _serviceProvider.GetService<LocationPagedViewModel>();
                return viewModel;
            },
            typeof(LocationPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Product,
            () =>
            {
                var viewModel = _serviceProvider.GetService<ProductPagedViewModel>();
                return viewModel;
            },
            typeof(ProductPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Supplier,
            () =>
            {
                var viewModel = _serviceProvider.GetService<SupplierPagedViewModel>();
                return viewModel;
            },
            typeof(SupplierPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Warehouse,
            () =>
            {
                var viewModel = _serviceProvider.GetService<WarehousePagedViewModel>();
                return viewModel;
            }, typeof(WarehousePagedView)));

            //DataDictionaryPagedViewModel
            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_DataDictionary,
            () =>
            {
                var viewModel = _serviceProvider.GetService<DataDictionaryPagedViewModel>();
                return viewModel;
            }, typeof(DataDictionaryPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_InspectionItem,
            () =>
            {
                var viewModel = _serviceProvider.GetService<InspectionItemPagedViewModel>();
                return viewModel;
            }, typeof(InspectionItemPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Standard,
            () =>
            {
                var viewModel = _serviceProvider.GetService<StandardPagedViewModel>();
                return viewModel;
            }, typeof(StandardPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_InspectionTask,
            () =>
            {
                var viewModel = _serviceProvider.GetService<InspectionTaskPagedViewModel>();
                return viewModel;
            }, typeof(InspectionTaskPagedView)));

            //注册Lims_InspectionTaskDashboard
            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_InspectionTaskDashboard,
            () =>
            {
                var viewModel = _serviceProvider.GetService<InspectionTaskViewModel>();
                return viewModel;
            }, typeof(InspectionTaskView)));



            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Inventory,
            () =>
            {
                var viewModel = _serviceProvider.GetService<InventoryPagedViewModel>();
                return viewModel;
            }, typeof(InventoryPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Out,
            () =>
            {
                var viewModel = _serviceProvider.GetService<InventoryOutPagedViewModel>();
                return viewModel;
            }, typeof(InventoryOutPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Store,
            () =>
            {
                var viewModel = _serviceProvider.GetService<InventoryStorePagedViewModel>();
                return viewModel;
            }, typeof(InventoryStorePagedView)));

            //注册InventoryCheck
            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_InventoryCheck,
            () =>
            {
                var viewModel = _serviceProvider.GetService<InventoryCheckPagedViewModel>();
                return viewModel;
            }, typeof(InventoryCheckPagedView)));

            //注册InventoryLog
            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_InventoryLog,
            () =>
            {
                var viewModel = _serviceProvider.GetService<InventoryLogPagedViewModel>();
                return viewModel;
            }, typeof(InventoryLogPagedView)));

            //注册Lims_SafetyStock
            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_SafetyStock,
            () =>
            {
                var viewModel = _serviceProvider.GetService<SafetyStockPagedViewModel>();
                return viewModel;
            }, typeof(SafetyStockPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Sample,
            () =>
            {
                var viewModel = _serviceProvider.GetService<SamplePagedViewModel>();
                return viewModel;
            }, typeof(SamplePagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Record,
            () =>
            {
                var viewModel = _serviceProvider.GetService<RecordPagedViewModel>();
                return viewModel;
            }, typeof(RecordPagedView)));

            //注册Lims_RecordRecordItems
            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_RecordRecordItems,
            () =>
            {
                var viewModel = _serviceProvider.GetService<RecordItemsPagedViewModel>();
                return viewModel;
            }, typeof(RecordItemsPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Equipment,
            () =>
            {
                var viewModel = _serviceProvider.GetService<EquipmentPagedViewModel>();
                return viewModel;
            }, typeof(EquipmentPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Maintenance,
            () =>
            {
                var viewModel = _serviceProvider.GetService<MaintenancePagedViewModel>();
                return viewModel;
            }, typeof(MaintenancePagedView)));

            //注册校准记录
            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Calibration,
            () =>
            {
                var viewModel = _serviceProvider.GetService<CalibrationPagedViewModel>();
                return viewModel;
            }, typeof(CalibrationPagedView)));

            //注册维修记录
            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_Repair,
            () =>
            {
                var viewModel = _serviceProvider.GetService<RepairPagedViewModel>();
                return viewModel;
            }, typeof(RepairPagedView)));

            //注册使用记录
            _uiModuleManager.Register(RegionNames.MainContentRegion, new DevExpress.Mvvm.ModuleInjection.Module(LimsUIViewKeys.Lims_UsageHistory,
            () =>
            {
                var viewModel = _serviceProvider.GetService<UsageHistoryPagedViewModel>();
                return viewModel;
            }, typeof(UsageHistoryPagedView)));


        }
    }
}
