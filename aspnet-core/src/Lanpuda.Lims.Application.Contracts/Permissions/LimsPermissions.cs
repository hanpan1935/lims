using Volo.Abp.Reflection;

namespace Lanpuda.Lims.Permissions;

public class LimsPermissions
{
    public const string GroupName = "Lims";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(LimsPermissions));
    }


    public const string Sample_Default = GroupName + "_Sample";
    public const string Sample_Update = Sample_Default + "_Update";
    public const string Sample_Create = Sample_Default + "_Create";
    public const string Sample_Delete = Sample_Default + "_Delete";



    public const string Record_Default = GroupName + "_Record";
    public const string Record_Update = Record_Default + "_Update";
    public const string Record_Create = Record_Default + "_Create";
    public const string Record_Delete = Record_Default + "_Delete";


    public const string InspectionTask_Default = GroupName + "_InspectionTask";
    public const string InspectionTask_Update = InspectionTask_Default + "_Update";
    public const string InspectionTask_Create = InspectionTask_Default + "_Create";
    public const string InspectionTask_Result = InspectionTask_Default + "_Result";
    public const string InspectionTask_Delete = InspectionTask_Default + "_Delete";


    /// <summary>
    /// 检验方法
    /// </summary>
    public const string InspectionMethod_Default = GroupName + "_InspectionMethod";

    public const string InspectionItem_Default = GroupName + "_InspectionItem";
    public const string InspectionItem_Update = InspectionItem_Default + "_Update";
    public const string InspectionItem_Create = InspectionItem_Default + "_Create";
    public const string InspectionItem_Delete = InspectionItem_Default + "_Delete";

    public const string Standard_Default = GroupName + "_Standard";
    public const string Standard_Update = Standard_Default + "_Update";
    public const string Standard_Create = Standard_Default + "_Create";
    public const string Standard_Delete = Standard_Default + "_Delete";


    /// <summary>
    /// 设备管理
    /// </summary>
    public const string EquipmentManagement_Default = GroupName + "_EquipmentManagement";


    public const string Equipment_Default = GroupName + "_Equipment";
    public const string Equipment_Update = Equipment_Default + "_Update";
    public const string Equipment_Create = Equipment_Default + "_Create";
    public const string Equipment_Delete = Equipment_Default + "_Delete";


    public const string Calibration_Default = GroupName + "_Calibration";
    public const string Calibration_Update = Calibration_Default + "_Update";
    public const string Calibration_Create = Calibration_Default + "_Create";
    public const string Calibration_Delete = Calibration_Default + "_Delete";

    public const string Maintenance_Default = GroupName + "_Maintenance";
    public const string Maintenance_Update = Maintenance_Default + "_Update";
    public const string Maintenance_Create = Maintenance_Default + "_Create";
    public const string Maintenance_Delete = Maintenance_Default + "_Delete";


    public const string Repair_Default = GroupName + "_Repair";
    public const string Repair_Update = Repair_Default + "_Update";
    public const string Repair_Create = Repair_Default + "_Create";
    public const string Repair_Delete = Repair_Default + "_Delete";


    public const string UsageHistory_Default = GroupName + "_UsageHistory";
    public const string UsageHistory_Update = UsageHistory_Default + "_Update";
    public const string UsageHistory_Create = UsageHistory_Default + "_Create";
    public const string UsageHistory_Delete = UsageHistory_Default + "_Delete";


    /// <summary>
    /// 库存管理
    /// </summary>
    public const string InventoryManagement_Default = GroupName + "_InventoryManagement";

    public const string Inventory_Default = GroupName + "_Inventory";
    public const string Inventory_Update = Inventory_Default + "_Update";
    public const string Inventory_Create = Inventory_Default + "_Create";
    public const string Inventory_Delete = Inventory_Default + "_Delete";

    public const string InventoryLog_Default = GroupName + "_InventoryLog";
    public const string InventoryLog_Update = InventoryLog_Default + "_Update";
    public const string InventoryLog_Create = InventoryLog_Default + "_Create";
    public const string InventoryLog_Delete = InventoryLog_Default + "_Delete";


    public const string InventoryOut_Default = GroupName + "_InventoryOut";
    public const string InventoryOut_Update = InventoryOut_Default + "_Update";
    public const string InventoryOut_Create = InventoryOut_Default + "_Create";
    public const string InventoryOut_Delete = InventoryOut_Default + "_Delete";
    public const string InventoryOut_Out = InventoryOut_Default + "_Out";

    public const string InventoryStore_Default = GroupName + "_InventoryStore";
    public const string InventoryStore_Update = InventoryStore_Default + "_Update";
    public const string InventoryStore_Create = InventoryStore_Default + "_Create";
    public const string InventoryStore_Store =  InventoryStore_Default + "_Store";
    public const string InventoryStore_Delete = InventoryStore_Default + "_Delete";

    /// <summary>
    /// 基础信息
    /// </summary>
    public const string BasicInfo_Default = GroupName + "_BasicInfo";

    public const string DictionaryData_Default = GroupName + "_DictionaryData";
    public const string DictionaryData_Update = DictionaryData_Default + "_Update";
    public const string DictionaryData_Create = DictionaryData_Default + "_Create";
    public const string DictionaryData_Delete = DictionaryData_Default + "_Delete";


    public const string Customer_Default = GroupName + "_Customer";
    public const string Customer_Update = Customer_Default + "_Update";
    public const string Customer_Create = Customer_Default + "_Create";
    public const string Customer_Delete = Customer_Default + "_Delete";

    public const string Location_Default = GroupName + "_Location";
    public const string Location_Update = Location_Default + "_Update";
    public const string Location_Create = Location_Default + "_Create";
    public const string Location_Delete = Location_Default + "_Delete";

    public const string Product_Default = GroupName + "_Product";
    public const string Product_Update = Product_Default + "_Update";
    public const string Product_Create = Product_Default + "_Create";
    public const string Product_Delete = Product_Default + "_Delete";

    public const string Supplier_Default = GroupName + "_Supplier";
    public const string Supplier_Update = Supplier_Default + "Supplier_Update";
    public const string Supplier_Create = Supplier_Default + "Supplier_Create";
    public const string Supplier_Delete = Supplier_Default + "Supplier_Delete";

    public const string Warehouse_Default = GroupName + "_Warehouse";
    public const string Warehouse_Update = Warehouse_Default + "_Update";
    public const string Warehouse_Create = Warehouse_Default + "_Create";
    public const string Warehouse_Delete = Warehouse_Default + "_Delete";
}
