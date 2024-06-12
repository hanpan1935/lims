using Lanpuda.Lims.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Lanpuda.Lims.Permissions;

public class LimsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LimsPermissions.GroupName, L("Permission:Lims"));

        //样品管理
        var samplePermission = myGroup.AddPermission(LimsPermissions.Sample_Default, L("Permission:Sample"));
        samplePermission.AddChild(LimsPermissions.Sample_Create, L("Permission:Create"));
        samplePermission.AddChild(LimsPermissions.Sample_Update, L("Permission:Update"));
        samplePermission.AddChild(LimsPermissions.Sample_Delete, L("Permission:Delete"));

        //检验数据
        var recordPermission = myGroup.AddPermission(LimsPermissions.Record_Default, L("Permission:Record"));
        recordPermission.AddChild(LimsPermissions.Record_Create, L("Permission:Create"));
        recordPermission.AddChild(LimsPermissions.Record_Update, L("Permission:Update"));
        recordPermission.AddChild(LimsPermissions.Record_Delete, L("Permission:Delete"));


        //检验任务
        var inspectionTaskPermission = myGroup.AddPermission(LimsPermissions.InspectionTask_Default, L("Permission:InspectionTask"));
        inspectionTaskPermission.AddChild(LimsPermissions.InspectionTask_Create, L("Permission:Create"));
        inspectionTaskPermission.AddChild(LimsPermissions.InspectionTask_Update, L("Permission:Update"));
        inspectionTaskPermission.AddChild(LimsPermissions.InspectionTask_Result, L("Permission:Result"));
        inspectionTaskPermission.AddChild(LimsPermissions.InspectionTask_Delete, L("Permission:Delete"));


        //检验方法
        var inspectionMethodPermission = myGroup.AddPermission(LimsPermissions.InspectionMethod_Default, L("Permission:InspectionMethod"));
        
        var inspectionItem = inspectionMethodPermission.AddChild(LimsPermissions.InspectionItem_Default, L("Permission:InspectionItem"));
        inspectionItem.AddChild(LimsPermissions.InspectionItem_Create, L("Permission:Create"));
        inspectionItem.AddChild(LimsPermissions.InspectionItem_Update, L("Permission:Update"));
        inspectionItem.AddChild(LimsPermissions.InspectionItem_Delete, L("Permission:Delete"));

        var standard = inspectionMethodPermission.AddChild(LimsPermissions.Standard_Default, L("Permission:Standard"));
        standard.AddChild(LimsPermissions.Standard_Create, L("Permission:Create"));
        standard.AddChild(LimsPermissions.Standard_Update, L("Permission:Update"));
        standard.AddChild(LimsPermissions.Standard_Delete, L("Permission:Delete"));

        //设备管理
        var equipmentManagementPermission = myGroup.AddPermission(LimsPermissions.EquipmentManagement_Default, L("Permission:EquipmentManagement"));


        var equipmentPermission = equipmentManagementPermission.AddChild(LimsPermissions.Equipment_Default, L("Permission:Equipment"));
        equipmentPermission.AddChild(LimsPermissions.Equipment_Create, L("Permission:Create"));
        equipmentPermission.AddChild(LimsPermissions.Equipment_Update, L("Permission:Update"));
        equipmentPermission.AddChild(LimsPermissions.Equipment_Delete, L("Permission:Delete"));


        var maintenancePermission = equipmentManagementPermission.AddChild(LimsPermissions.Maintenance_Default, L("Permission:Maintenance"));
        maintenancePermission.AddChild(LimsPermissions.Maintenance_Create, L("Permission:Create"));
        maintenancePermission.AddChild(LimsPermissions.Maintenance_Update, L("Permission:Update"));
        maintenancePermission.AddChild(LimsPermissions.Maintenance_Delete, L("Permission:Delete"));

        var calibrationPermission = equipmentManagementPermission.AddChild(LimsPermissions.Calibration_Default, L("Permission:Calibration"));
        calibrationPermission.AddChild(LimsPermissions.Calibration_Create, L("Permission:Create"));
        calibrationPermission.AddChild(LimsPermissions.Calibration_Update, L("Permission:Update"));
        calibrationPermission.AddChild(LimsPermissions.Calibration_Delete, L("Permission:Delete"));

        var repairPermission = equipmentManagementPermission.AddChild(LimsPermissions.Repair_Default, L("Permission:Repair"));
        repairPermission.AddChild(LimsPermissions.Repair_Create, L("Permission:Create"));
        repairPermission.AddChild(LimsPermissions.Repair_Update, L("Permission:Update"));
        repairPermission.AddChild(LimsPermissions.Repair_Delete, L("Permission:Delete"));

        var usageHistoryPermission = equipmentManagementPermission.AddChild(LimsPermissions.UsageHistory_Default, L("Permission:UsageHistory"));
        usageHistoryPermission.AddChild(LimsPermissions.UsageHistory_Create, L("Permission:Create"));
        usageHistoryPermission.AddChild(LimsPermissions.UsageHistory_Update, L("Permission:Update"));
        usageHistoryPermission.AddChild(LimsPermissions.UsageHistory_Delete, L("Permission:Delete"));


        //库存管理
        var inventoryManagementPermission = myGroup.AddPermission(LimsPermissions.InventoryManagement_Default, L("Permission:InventoryManagement"));

        var inventoryPermission = inventoryManagementPermission.AddChild(LimsPermissions.Inventory_Default, L("Permission:Inventory"));
        //inventoryPermission.AddChild(LimsPermissions.Inventory.Create, L("Permission:Create"));
        //inventoryPermission.AddChild(LimsPermissions.Inventory.Update, L("Permission:Update"));
        //inventoryPermission.AddChild(LimsPermissions.Inventory.Delete, L("Permission:Delete"));

        var inventoryOutPermission = inventoryManagementPermission.AddChild(LimsPermissions.InventoryOut_Default, L("Permission:InventoryOut"));
        inventoryOutPermission.AddChild(LimsPermissions.InventoryOut_Create, L("Permission:Create"));
        inventoryOutPermission.AddChild(LimsPermissions.InventoryOut_Update, L("Permission:Update"));
        inventoryOutPermission.AddChild(LimsPermissions.InventoryOut_Out, L("Permission:Out"));
        inventoryOutPermission.AddChild(LimsPermissions.InventoryOut_Delete, L("Permission:Delete"));

        var inventoryStorePermission = inventoryManagementPermission.AddChild(LimsPermissions.InventoryStore_Default, L("Permission:InventoryStore"));
        inventoryStorePermission.AddChild(LimsPermissions.InventoryStore_Create, L("Permission:Create"));
        inventoryStorePermission.AddChild(LimsPermissions.InventoryStore_Update, L("Permission:Update"));
        inventoryStorePermission.AddChild(LimsPermissions.InventoryStore_Store, L("Permission:Store"));
        inventoryStorePermission.AddChild(LimsPermissions.InventoryStore_Delete, L("Permission:Delete"));

        var inventoryLogPermission = inventoryManagementPermission.AddChild(LimsPermissions.InventoryLog_Default, L("Permission:InventoryLog"));
        //inventoryLogPermission.AddChild(LimsPermissions.InventoryLog.Create, L("Permission:Create"));
        //inventoryLogPermission.AddChild(LimsPermissions.InventoryLog.Update, L("Permission:Update"));
        //inventoryLogPermission.AddChild(LimsPermissions.InventoryLog.Delete, L("Permission:Delete"));


        //基础数据

        var basicInfoPermission = myGroup.AddPermission(LimsPermissions.BasicInfo_Default, L("Permission:BasicInfo"));

        var productPermission = basicInfoPermission.AddChild(LimsPermissions.Product_Default, L("Permission:Product"));
        productPermission.AddChild(LimsPermissions.Product_Create, L("Permission:Create"));
        productPermission.AddChild(LimsPermissions.Product_Update, L("Permission:Update"));
        productPermission.AddChild(LimsPermissions.Product_Delete, L("Permission:Delete"));

        var customerPermission = basicInfoPermission.AddChild(LimsPermissions.Customer_Default, L("Permission:Customer"));
        customerPermission.AddChild(LimsPermissions.Customer_Create, L("Permission:Create"));
        customerPermission.AddChild(LimsPermissions.Customer_Update, L("Permission:Update"));
        customerPermission.AddChild(LimsPermissions.Customer_Delete, L("Permission:Delete"));

        var supplierPermission = basicInfoPermission.AddChild(LimsPermissions.Supplier_Default, L("Permission:Supplier"));
        supplierPermission.AddChild(LimsPermissions.Supplier_Create, L("Permission:Create"));
        supplierPermission.AddChild(LimsPermissions.Supplier_Update, L("Permission:Update"));
        supplierPermission.AddChild(LimsPermissions.Supplier_Delete, L("Permission:Delete"));

        var warehousePermission = basicInfoPermission.AddChild(LimsPermissions.Warehouse_Default, L("Permission:Warehouse"));
        warehousePermission.AddChild(LimsPermissions.Warehouse_Create, L("Permission:Create"));
        warehousePermission.AddChild(LimsPermissions.Warehouse_Update, L("Permission:Update"));
        warehousePermission.AddChild(LimsPermissions.Warehouse_Delete, L("Permission:Delete"));

        var locationPermission = basicInfoPermission.AddChild(LimsPermissions.Location_Default, L("Permission:Location"));
        locationPermission.AddChild(LimsPermissions.Location_Create, L("Permission:Create"));
        locationPermission.AddChild(LimsPermissions.Location_Update, L("Permission:Update"));
        locationPermission.AddChild(LimsPermissions.Location_Delete, L("Permission:Delete"));

        var dictionaryPermission = basicInfoPermission.AddChild(LimsPermissions.DictionaryData_Default, L("Permission:DictionaryType"));
        dictionaryPermission.AddChild(LimsPermissions.DictionaryData_Create, L("Permission:Create"));
        dictionaryPermission.AddChild(LimsPermissions.DictionaryData_Update, L("Permission:Update"));
        dictionaryPermission.AddChild(LimsPermissions.DictionaryData_Delete, L("Permission:Delete"));
       
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LimsResource>(name);
    }
}
