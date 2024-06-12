using Lanpuda.Client.Theme;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Lanpuda.Client.Theme.Services.MenuServices;
using Lanpuda.Client.Theme.Entities;
using Lanpuda.Lims.Permissions;

namespace Lanpuda.Lims.UI
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(ThemeModule))]
    [DependsOn(typeof(LimsHttpApiClientModule))]
    public class LimsUIModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<LimsUIBootstraper>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                //Add all mappings defined in the assembly of the MyModule class
                options.AddMaps<LimsUIModule>();
            });

            JsonSerializerOptions options = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }


        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPostApplicationInitialization(context);
            var bootstraper = context.ServiceProvider.GetRequiredService<LimsUIBootstraper>();
            bootstraper.Load();

            //
            var menuService = context.ServiceProvider.GetRequiredService<IMenuService>();
            List<MenuItem> menuItems = new List<MenuItem>();
            menuService.SetMenusByModuleKey("LIMSUI", menuItems);
            //首页
            MenuItem homeMenu = new MenuItem() { MenuHeader = "LIMS首页", TargetKey = LimsUIViewKeys.Lims_Home};
            menuItems.Add(homeMenu);

            
            //样品管理
            MenuItem sampleMenu = new MenuItem() { MenuHeader = "样品管理", TargetKey = LimsUIViewKeys.Lims_Sample, PermissionName = LimsPermissions.Sample_Default };
            menuItems.Add(sampleMenu);

            //检验数据
            MenuItem recordMenu = new MenuItem() { MenuHeader = "检验记录", TargetKey = LimsUIViewKeys.Lims_Record, PermissionName = LimsPermissions.Record_Default };
            menuItems.Add(recordMenu);


            //检验任务
            MenuItem taskMenu = new MenuItem() { MenuHeader = "检验任务", TargetKey = LimsUIViewKeys.Lims_InspectionTask, PermissionName = LimsPermissions.InspectionTask_Default };
            menuItems.Add(taskMenu);

            //检验方法
            MenuItem methodsMenu = new MenuItem() { MenuHeader = "检验方法", PermissionName = LimsPermissions.InspectionMethod_Default };
            menuItems.Add(methodsMenu);
            MenuItem inspectionItemMenu = new MenuItem() { MenuHeader = "检验项目", TargetKey = LimsUIViewKeys.Lims_InspectionItem, PermissionName = LimsPermissions.InspectionItem_Default };
            MenuItem standardMenu = new MenuItem() { MenuHeader = "检验标准", TargetKey = LimsUIViewKeys.Lims_Standard, PermissionName = LimsPermissions.Standard_Default };
            methodsMenu.Children.Add(inspectionItemMenu);
            methodsMenu.Children.Add(standardMenu);


            //设备管理
            MenuItem equipmentMenu = new MenuItem() { MenuHeader = "设备管理", PermissionName = LimsPermissions.EquipmentManagement_Default };
            menuItems.Add(equipmentMenu);
            MenuItem equipmentMenu1 = new MenuItem() { MenuHeader = "设备信息", TargetKey = LimsUIViewKeys.Lims_Equipment, PermissionName = LimsPermissions.Equipment_Default };
            MenuItem equipmentMenu2 = new MenuItem() { MenuHeader = "维护记录", TargetKey = LimsUIViewKeys.Lims_Maintenance, PermissionName = LimsPermissions.Maintenance_Default };
            MenuItem equipmentMenu3 = new MenuItem() { MenuHeader = "校准记录", TargetKey = LimsUIViewKeys.Lims_Calibration, PermissionName = LimsPermissions.Calibration_Default };
            MenuItem equipmentMenu4 = new MenuItem() { MenuHeader = "维修记录", TargetKey = LimsUIViewKeys.Lims_Repair, PermissionName = LimsPermissions.Repair_Default };
            MenuItem equipmentMenu5 = new MenuItem() { MenuHeader = "使用记录", TargetKey = LimsUIViewKeys.Lims_UsageHistory, PermissionName = LimsPermissions.UsageHistory_Default };
            equipmentMenu.Children.Add(equipmentMenu1);
            equipmentMenu.Children.Add(equipmentMenu2);
            equipmentMenu.Children.Add(equipmentMenu3);
            equipmentMenu.Children.Add(equipmentMenu4);
            equipmentMenu.Children.Add(equipmentMenu5);

            //库存管理
            MenuItem inventoryMenu = new MenuItem() { MenuHeader = "库存管理", PermissionName = LimsPermissions.InventoryManagement_Default };
            menuItems.Add(inventoryMenu);
            MenuItem inventoryMenu0 = new MenuItem() { MenuHeader = "库存查询", TargetKey = LimsUIViewKeys.Lims_Inventory, PermissionName = LimsPermissions.Inventory_Default };
            MenuItem inventoryMenu1 = new MenuItem() { MenuHeader = "入库操作", TargetKey = LimsUIViewKeys.Lims_Store, PermissionName = LimsPermissions.InventoryStore_Default };
            MenuItem inventoryMenu2 = new MenuItem() { MenuHeader = "出库操作", TargetKey = LimsUIViewKeys.Lims_Out, PermissionName = LimsPermissions.InventoryOut_Default };
            //MenuItem inventoryMenu3 = new MenuItem() { MenuHeader = "库存盘点", TargetKey = LimsUIViewKeys.Lims_InventoryCheck };
            MenuItem inventoryMenu4 = new MenuItem() { MenuHeader = "库存流水", TargetKey = LimsUIViewKeys.Lims_InventoryLog, PermissionName = LimsPermissions.InventoryLog_Default };
            //MenuItem inventoryMenu5 = new MenuItem() { MenuHeader = "安全库存", TargetKey = LimsUIViewKeys.Lims_SafetyStock };
            inventoryMenu.Children.Add(inventoryMenu0);
            inventoryMenu.Children.Add(inventoryMenu1);
            inventoryMenu.Children.Add(inventoryMenu2);
            //inventoryMenu.Children.Add(inventoryMenu3);
            inventoryMenu.Children.Add(inventoryMenu4);
            //inventoryMenu.Children.Add(inventoryMenu5);

            //基础数据
            MenuItem basicDataMenu = new MenuItem() { MenuHeader = "基础数据", PermissionName = LimsPermissions.BasicInfo_Default };
            menuItems.Add(basicDataMenu);
            MenuItem productMenu = new MenuItem() { MenuHeader = "产品物料", TargetKey = LimsUIViewKeys.Lims_Product, PermissionName = LimsPermissions.Product_Default };
            MenuItem customerMenu = new MenuItem() { MenuHeader = "客户信息" ,TargetKey = LimsUIViewKeys.Lims_Customer, PermissionName = LimsPermissions.Customer_Default };
            MenuItem supplierMenu = new MenuItem() { MenuHeader = "供应商", TargetKey = LimsUIViewKeys.Lims_Supplier, PermissionName = LimsPermissions.Supplier_Default };
            MenuItem warehouseMenu = new MenuItem() { MenuHeader = "仓库设置", TargetKey = LimsUIViewKeys.Lims_Warehouse, PermissionName = LimsPermissions.Warehouse_Default };
            MenuItem locationMenu = new MenuItem() { MenuHeader = "库位设置", TargetKey = LimsUIViewKeys.Lims_Location, PermissionName = LimsPermissions.Location_Default };
            //DataDictionary
            MenuItem dataDictionaryMenu = new MenuItem() { MenuHeader = "数据字典" , TargetKey = LimsUIViewKeys.Lims_DataDictionary, PermissionName = LimsPermissions.DictionaryData_Default };



            basicDataMenu.Children.Add(productMenu);
            basicDataMenu.Children.Add(customerMenu);
            basicDataMenu.Children.Add(supplierMenu);
            basicDataMenu.Children.Add(warehouseMenu);
            basicDataMenu.Children.Add(locationMenu);
            basicDataMenu.Children.Add(dataDictionaryMenu);

            //MenuItem productCategoryMenu = new MenuItem() { MenuHeader = "产品信息", PermissionName = ERPPermissions.Product.Default, TargetKey = ERPUIViewKeys.Product_Paged };
            //MenuItem productUnit = new MenuItem() { MenuHeader = "产品单位", PermissionName = ERPPermissions.ProductUnit.Default, TargetKey = ERPUIViewKeys.ProductUnit_Paged };
            //MenuItem product = new MenuItem() { MenuHeader = "产品分类", PermissionName = ERPPermissions.ProductCategory.Default, TargetKey = ERPUIViewKeys.ProductCategory_Paged };

            //basicDataSettingMenu.Children.Add(productCategoryMenu);
            //basicDataSettingMenu.Children.Add(productUnit);
            //basicDataSettingMenu.Children.Add(product);
            //basicDataMenu.Children.Add(basicDataSettingMenu);
            //menuItems.Add(basicDataMenu);


        }
    }
}
