using Lanpuda.Lims.Calibrations;
using Lanpuda.Lims.Customers;
using Lanpuda.Lims.DataDictionaries;
using Lanpuda.Lims.Equipments;
using Lanpuda.Lims.InspectionItems;
using Lanpuda.Lims.InspectionTasks;
using Lanpuda.Lims.Inventories;
using Lanpuda.Lims.InventoryLogs;
using Lanpuda.Lims.InventoryOuts;
using Lanpuda.Lims.InventoryStores;
using Lanpuda.Lims.Locations;
using Lanpuda.Lims.Maintenances;
using Lanpuda.Lims.Products;
using Lanpuda.Lims.Records;
using Lanpuda.Lims.Repairs;
using Lanpuda.Lims.Samples;
using Lanpuda.Lims.Standards;
using Lanpuda.Lims.Suppliers;
using Lanpuda.Lims.UsageHistories;
using Lanpuda.Lims.Warehouses;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Lanpuda.Lims.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class LimsDbContext :
    AbpDbContext<LimsDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion


    /// <summary>
    /// 
    /// </summary>
    public DbSet<Calibration> Calibrations { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Customer> Customers { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<DicEquipmentType> DicEquipmentTypes { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<DicProductType> DicProductTypes { get; set; }
    /// <summary>
    /// 综合判级
    /// </summary>
    public DbSet<DicRatingType> DicRatingTypes { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<DicSampleProperty> DicSampleProperties { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<DicSampleType> DicSampleTypes { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<DicStandardType> DicStandardTypes { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Equipment> Equipments { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<InspectionItem> InspectionItems { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<InspectionTask> InspectionTasks { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Inventory> Inventories { get; set; }
    /// <summary>
    /// 库存流水
    /// </summary>
    public DbSet<InventoryLog> InventoryLogs { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<InventoryOut> InventoryOuts { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<InventoryOutDetail> InventoryOutDetails { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<InventoryStore> InventoryStores { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<InventoryStoreDetail> InventoryStoreDetails { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Location> Locations { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Maintenance> Maintenances { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Product> Products { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Record> Records { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<RecordDetail> RecordDetails { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Repair> Repairs { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Sample> Samples { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Standard> Standards { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<StandardDetail> StandardDetails { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<UsageHistory> UsageHistories { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Supplier> Suppliers { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Warehouse> Warehouses { get; set; }

    public LimsDbContext(DbContextOptions<LimsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(LimsConsts.DbTablePrefix + "YourEntities", LimsConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Calibration>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Calibrations", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Number).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            //
            b.HasOne(q => q.Equipment).WithMany().HasForeignKey(q => q.EquipmentId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<Customer>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Customers", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Number).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.Property(q => q.FullName).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.Property(q => q.ShortName).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<DicEquipmentType>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "DicEquipmentTypes", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.DisplayValue).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<DicProductType>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "DicProductTypes", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();

            /* Configure more properties here */
            b.Property(q => q.DisplayValue).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<DicRatingType>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "DicRatingTypes", LimsDbProperties.DbSchema, table => table.HasComment("综合判级"));
            b.ConfigureByConvention();


            /* Configure more properties here */
            b.Property(q => q.DisplayValue).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<DicSampleProperty>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "DicSampleProperties", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();


            /* Configure more properties here */
            b.Property(q => q.DisplayValue).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<DicSampleType>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "DicSampleTypes", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();


            /* Configure more properties here */
            b.Property(q => q.DisplayValue).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<DicStandardType>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "DicStandardTypes", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();

            /* Configure more properties here */
            b.Property(q => q.DisplayValue).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<Equipment>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Equipment", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Name).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(q => q.DicEquipmentType).WithMany().HasForeignKey(q => q.DicEquipmentTypeId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<InspectionItem>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "InspectionItems", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.ShortName).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.Property(q => q.FullName).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.Property(q => q.Basis).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.Property(q => q.Unit).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);

            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);

        });


        builder.Entity<InspectionTask>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "InspectionTasks", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.RecordDetail).WithMany(m => m.InspectionTaskList).HasForeignKey(m => m.RecordDetailId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<Inventory>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Inventories", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<InventoryLog>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "InventoryLogs", LimsDbProperties.DbSchema, table => table.HasComment("库存流水"));
            b.ConfigureByConvention();

            /* Configure more properties here */
            b.Property(q => q.Number).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);

            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<InventoryOut>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "InventoryOuts", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Number).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasMany(m => m.Details).WithOne(m => m.InventoryOut).HasForeignKey(m => m.InventoryOutId).OnDelete(DeleteBehavior.Cascade);

            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<InventoryOutDetail>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "InventoryOutDetails", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.Restrict);

            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<InventoryStore>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "InventoryStores", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Number).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasMany(m => m.Details).WithOne(m => m.InventoryStore).HasForeignKey(m => m.InventoryStoreId).OnDelete(DeleteBehavior.Cascade);


            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<InventoryStoreDetail>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "InventoryStoreDetails", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.Restrict);

            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<Location>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Locations", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Name).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(m => m.Warehouse).WithMany(Warehouse => Warehouse.Locations).HasForeignKey(m => m.WarehouseId).OnDelete(DeleteBehavior.Restrict);

            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<Maintenance>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Maintenances", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Number).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(m => m.Equipment).WithMany().HasForeignKey(m => m.EquipmentId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);

        });


        builder.Entity<Product>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Products", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Name).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.Property(q => q.Unit).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            //
            b.HasOne(m => m.DicProductType).WithMany().HasForeignKey(m => m.DicProductTypeId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<Record>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Records", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Number).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(m => m.Sample).WithMany().HasForeignKey(m => m.SampleId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.DicRatingType).WithMany().HasForeignKey(m => m.DicRatingTypeId).OnDelete(DeleteBehavior.Restrict);
            b.HasMany(m => m.Details).WithOne(m => m.Record).HasForeignKey(m => m.RecordId).OnDelete(DeleteBehavior.Cascade);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<RecordDetail>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "RecordDetails", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.InspectionItem).WithMany().HasForeignKey(m => m.InspectionItemId).OnDelete(DeleteBehavior.Restrict);
            b.HasMany(b => b.InspectionTaskList).WithOne(m => m.RecordDetail).HasForeignKey(b => b.RecordDetailId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<Repair>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Repairs", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Number).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(m => m.Equipment).WithMany().HasForeignKey(m => m.EquipmentId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<Sample>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Samples", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Number).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);

            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.DicSampleType).WithMany().HasForeignKey(m => m.DicSampleTypeId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.DicSampleProperty).WithMany().HasForeignKey(m => m.DicSamplePropertyId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);

            b.HasOne(m => m.Customer).WithMany().HasForeignKey(m => m.CustomerId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.Supplier).WithMany().HasForeignKey(m => m.SupplierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<Standard>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Standards", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Description).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(m => m.DicStandardType).WithMany().HasForeignKey(m => m.DicStandardTypeId).OnDelete(DeleteBehavior.Restrict);
            b.HasMany(m => m.Details).WithOne(m => m.Standard).HasForeignKey(m => m.StandardId).OnDelete(DeleteBehavior.Cascade);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<StandardDetail>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "StandardDetails", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.InspectionItem).WithMany().HasForeignKey(m => m.InspectionItemId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<UsageHistory>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "UsageHistories", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.Number).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasOne(m => m.Equipment).WithMany().HasForeignKey(m => m.EquipmentId).OnDelete(DeleteBehavior.Restrict);

            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<Supplier>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Suppliers", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(q => q.FullName).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.Property(q => q.ShortName).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);

            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


        builder.Entity<Warehouse>(b =>
        {
            b.ToTable(LimsDbProperties.DbTablePrefix + "Warehouses", LimsDbProperties.DbSchema, table => table.HasComment(""));
            b.ConfigureByConvention();

            /* Configure more properties here */
            b.Property(q => q.Name).IsRequired().HasMaxLength(LimsDbProperties.MaxTitleLength);
            b.HasMany(m => m.Locations).WithOne(m => m.Warehouse).HasForeignKey(m => m.WarehouseId).OnDelete(DeleteBehavior.Restrict);

            b.HasOne(q => q.Creator).WithMany().HasForeignKey(q => q.CreatorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.LastModifier).WithMany().HasForeignKey(m => m.LastModifierId).OnDelete(DeleteBehavior.Restrict);
        });


    }
}
