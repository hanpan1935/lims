using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanpuda.Lims.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpAuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationName = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ImpersonatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpersonatorUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImpersonatorTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpersonatorTenantName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "int", nullable: false),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CorrelationId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    HttpMethod = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Exceptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    HttpStatusCode = table.Column<int>(type: "int", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpAuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpBackgroundJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    JobArgs = table.Column<string>(type: "nvarchar(max)", maxLength: 1048576, nullable: false),
                    TryCount = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextTryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastTryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAbandoned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Priority = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)15),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpBackgroundJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpClaimTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    IsStatic = table.Column<bool>(type: "bit", nullable: false),
                    Regex = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    RegexDescription = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ValueType = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpClaimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatureGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatureGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DefaultValue = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsVisibleToClients = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableToHost = table.Column<bool>(type: "bit", nullable: false),
                    AllowedProviders = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ValueType = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatureValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatureValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpLinkUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpLinkUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpOrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(95)", maxLength: 95, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EntityVersion = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpOrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissionGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissionGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    MultiTenancySide = table.Column<byte>(type: "tinyint", nullable: false),
                    Providers = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    StateCheckers = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsStatic = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    EntityVersion = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSecurityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationName = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    Identity = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TenantName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CorrelationId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSecurityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSettingDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DefaultValue = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    IsVisibleToClients = table.Column<bool>(type: "bit", nullable: false),
                    Providers = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsInherited = table.Column<bool>(type: "bit", nullable: false),
                    IsEncrypted = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSettingDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpTenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EntityVersion = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpTenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserDelegations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserDelegations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsExternal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ShouldChangePasswordOnNextLogin = table.Column<bool>(type: "bit", nullable: false),
                    EntityVersion = table.Column<int>(type: "int", nullable: false),
                    LastPasswordChangeTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConsentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonWebKeySet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedirectUris = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Settings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descriptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resources = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpAuditLogActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuditLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MethodName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Parameters = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpAuditLogActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpAuditLogActions_AbpAuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AbpAuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpEntityChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChangeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeType = table.Column<byte>(type: "tinyint", nullable: false),
                    EntityTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    EntityTypeFullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEntityChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpEntityChanges_AbpAuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AbpAuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpOrganizationUnitRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpOrganizationUnitRoles", x => new { x.OrganizationUnitId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnitRoles_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpRoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpRoleClaims_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpTenantConnectionStrings",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpTenantConnectionStrings", x => new { x.TenantId, x.Name });
                    table.ForeignKey(
                        name: "FK_AbpTenantConnectionStrings_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpUserClaims_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(196)", maxLength: 196, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserLogins", x => new { x.UserId, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_AbpUserLogins_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserOrganizationUnits",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserOrganizationUnits", x => new { x.OrganizationUnitId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpUserOrganizationUnits_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AbpUserRoles_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpUserRoles_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AbpUserTokens_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lims_Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Manager = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ManagerTel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Consignee = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConsigneeTel = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Customers_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Customers_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_DicEquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayValue = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_DicEquipmentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_DicEquipmentTypes_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_DicEquipmentTypes_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_DicProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayValue = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_DicProductTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_DicProductTypes_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_DicProductTypes_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_DicRatingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayValue = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_DicRatingTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_DicRatingTypes_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_DicRatingTypes_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "综合判级");

            migrationBuilder.CreateTable(
                name: "Lims_DicSampleProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayValue = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_DicSampleProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_DicSampleProperties_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_DicSampleProperties_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_DicSampleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayValue = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_DicSampleTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_DicSampleTypes_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_DicSampleTypes_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_DicStandardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayValue = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_DicStandardTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_DicStandardTypes_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_DicStandardTypes_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_InventoryOuts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_InventoryOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryOuts_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryOuts_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_InventoryStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    SuccessfulTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_InventoryStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryStores_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryStores_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Manager = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ManagerTel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Suppliers_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Suppliers_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Warehouses_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Warehouses_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scopes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AbpEntityPropertyChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityChangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    OriginalValue = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    PropertyName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PropertyTypeFullName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEntityPropertyChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId",
                        column: x => x.EntityChangeId,
                        principalTable: "AbpEntityChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lims_Equipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MaintenancePeriod = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DicEquipmentTypeId = table.Column<int>(type: "int", nullable: true),
                    Spec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcquisitionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OperationManual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallationLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalibrationStandard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaintenanceStandard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Equipment_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Equipment_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Equipment_Lims_DicEquipmentTypes_DicEquipmentTypeId",
                        column: x => x.DicEquipmentTypeId,
                        principalTable: "Lims_DicEquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_Standards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DicStandardTypeId = table.Column<int>(type: "int", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Standards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Standards_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Standards_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Standards_Lims_DicStandardTypes_DicStandardTypeId",
                        column: x => x.DicStandardTypeId,
                        principalTable: "Lims_DicStandardTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Locations_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Locations_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Locations_Lims_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Lims_Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuthorizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedemptionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferenceId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lims_Calibrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalibrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CalibrationResult = table.Column<int>(type: "int", nullable: false),
                    NextCalibrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Person = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Standard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Calibrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Calibrations_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Calibrations_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Calibrations_Lims_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Lims_Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_InspectionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Basis = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultEquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_InspectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_InspectionItems_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InspectionItems_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InspectionItems_Lims_Equipment_DefaultEquipmentId",
                        column: x => x.DefaultEquipmentId,
                        principalTable: "Lims_Equipment",
                        principalColumn: "Id");
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_Maintenances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaintenanceType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Person = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpentTime = table.Column<float>(type: "real", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Result = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Maintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Maintenances_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Maintenances_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Maintenances_Lims_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Lims_Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_Repairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RepairDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepairResult = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Person = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepairTime = table.Column<float>(type: "real", nullable: true),
                    RepairCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RepairWorkOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepairDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepairStandard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Repairs_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Repairs_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Repairs_Lims_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Lims_Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_UsageHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsageHistoryType = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Person = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_UsageHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_UsageHistories_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_UsageHistories_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_UsageHistories_Lims_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Lims_Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DicProductTypeId = table.Column<int>(type: "int", nullable: true),
                    Spec = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    StandardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Products_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Products_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Products_Lims_DicProductTypes_DicProductTypeId",
                        column: x => x.DicProductTypeId,
                        principalTable: "Lims_DicProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Products_Lims_Standards_StandardId",
                        column: x => x.StandardId,
                        principalTable: "Lims_Standards",
                        principalColumn: "Id");
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_StandardDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StandardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InspectionItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinValue = table.Column<double>(type: "float", nullable: true),
                    HasMinValue = table.Column<bool>(type: "bit", nullable: false),
                    MaxValue = table.Column<double>(type: "float", nullable: true),
                    HasMaxValue = table.Column<bool>(type: "bit", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_StandardDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_StandardDetails_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_StandardDetails_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_StandardDetails_Lims_InspectionItems_InspectionItemId",
                        column: x => x.InspectionItemId,
                        principalTable: "Lims_InspectionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_StandardDetails_Lims_Standards_StandardId",
                        column: x => x.StandardId,
                        principalTable: "Lims_Standards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_Inventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Inventories_Lims_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Lims_Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Inventories_Lims_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Lims_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_InventoryLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LotNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InQuantity = table.Column<double>(type: "float", nullable: false),
                    OutQuantity = table.Column<double>(type: "float", nullable: false),
                    AfterQuantity = table.Column<double>(type: "float", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_InventoryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryLogs_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryLogs_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryLogs_Lims_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Lims_Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryLogs_Lims_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Lims_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "库存流水");

            migrationBuilder.CreateTable(
                name: "Lims_InventoryOutDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryOutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_InventoryOutDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryOutDetails_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryOutDetails_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryOutDetails_Lims_InventoryOuts_InventoryOutId",
                        column: x => x.InventoryOutId,
                        principalTable: "Lims_InventoryOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryOutDetails_Lims_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Lims_Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryOutDetails_Lims_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Lims_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_InventoryStoreDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryStoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_InventoryStoreDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryStoreDetails_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryStoreDetails_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryStoreDetails_Lims_InventoryStores_InventoryStoreId",
                        column: x => x.InventoryStoreId,
                        principalTable: "Lims_InventoryStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryStoreDetails_Lims_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Lims_Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InventoryStoreDetails_Lims_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Lims_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_Samples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DicSampleTypeId = table.Column<int>(type: "int", nullable: true),
                    DicSamplePropertyId = table.Column<int>(type: "int", nullable: true),
                    SampleTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SampleCount = table.Column<double>(type: "float", nullable: true),
                    ExpireTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Samples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Samples_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Samples_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Samples_Lims_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Lims_Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Samples_Lims_DicSampleProperties_DicSamplePropertyId",
                        column: x => x.DicSamplePropertyId,
                        principalTable: "Lims_DicSampleProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Samples_Lims_DicSampleTypes_DicSampleTypeId",
                        column: x => x.DicSampleTypeId,
                        principalTable: "Lims_DicSampleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Samples_Lims_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Lims_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Samples_Lims_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Lims_Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_Records",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SampleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DicRatingTypeId = table.Column<int>(type: "int", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_Records_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Records_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Records_Lims_DicRatingTypes_DicRatingTypeId",
                        column: x => x.DicRatingTypeId,
                        principalTable: "Lims_DicRatingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_Records_Lims_Samples_SampleId",
                        column: x => x.SampleId,
                        principalTable: "Lims_Samples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_RecordDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InspectionItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinValue = table.Column<double>(type: "float", nullable: true),
                    HasMinValue = table.Column<bool>(type: "bit", nullable: false),
                    MaxValue = table.Column<double>(type: "float", nullable: true),
                    HasMaxValue = table.Column<bool>(type: "bit", nullable: false),
                    ResultValue = table.Column<double>(type: "float", nullable: true),
                    IsQualified = table.Column<bool>(type: "bit", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_RecordDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_RecordDetails_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_RecordDetails_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_RecordDetails_Lims_InspectionItems_InspectionItemId",
                        column: x => x.InspectionItemId,
                        principalTable: "Lims_InspectionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_RecordDetails_Lims_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Lims_Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Lims_InspectionTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InspectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Inspector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultValue = table.Column<double>(type: "float", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lims_InspectionTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lims_InspectionTasks_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InspectionTasks_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lims_InspectionTasks_Lims_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Lims_Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lims_InspectionTasks_Lims_RecordDetails_RecordDetailId",
                        column: x => x.RecordDetailId,
                        principalTable: "Lims_RecordDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogActions_AuditLogId",
                table: "AbpAuditLogActions",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_ExecutionTime",
                table: "AbpAuditLogActions",
                columns: new[] { "TenantId", "ServiceName", "MethodName", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogs_TenantId_ExecutionTime",
                table: "AbpAuditLogs",
                columns: new[] { "TenantId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogs_TenantId_UserId_ExecutionTime",
                table: "AbpAuditLogs",
                columns: new[] { "TenantId", "UserId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime",
                table: "AbpBackgroundJobs",
                columns: new[] { "IsAbandoned", "NextTryTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityChanges_AuditLogId",
                table: "AbpEntityChanges",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId",
                table: "AbpEntityChanges",
                columns: new[] { "TenantId", "EntityTypeFullName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityPropertyChanges_EntityChangeId",
                table: "AbpEntityPropertyChanges",
                column: "EntityChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatureGroups_Name",
                table: "AbpFeatureGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatures_GroupName",
                table: "AbpFeatures",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatures_Name",
                table: "AbpFeatures",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatureValues_Name_ProviderName_ProviderKey",
                table: "AbpFeatureValues",
                columns: new[] { "Name", "ProviderName", "ProviderKey" },
                unique: true,
                filter: "[ProviderName] IS NOT NULL AND [ProviderKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_TargetTenantId",
                table: "AbpLinkUsers",
                columns: new[] { "SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId" },
                unique: true,
                filter: "[SourceTenantId] IS NOT NULL AND [TargetTenantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId",
                table: "AbpOrganizationUnitRoles",
                columns: new[] { "RoleId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_Code",
                table: "AbpOrganizationUnits",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_ParentId",
                table: "AbpOrganizationUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey",
                table: "AbpPermissionGrants",
                columns: new[] { "TenantId", "Name", "ProviderName", "ProviderKey" },
                unique: true,
                filter: "[TenantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissionGroups_Name",
                table: "AbpPermissionGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_GroupName",
                table: "AbpPermissions",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_Name",
                table: "AbpPermissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoleClaims_RoleId",
                table: "AbpRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoles_NormalizedName",
                table: "AbpRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_Action",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "Action" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_ApplicationName",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "ApplicationName" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_Identity",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "Identity" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_UserId",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSettingDefinitions_Name",
                table: "AbpSettingDefinitions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpSettings_Name_ProviderName_ProviderKey",
                table: "AbpSettings",
                columns: new[] { "Name", "ProviderName", "ProviderKey" },
                unique: true,
                filter: "[ProviderName] IS NOT NULL AND [ProviderKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_Name",
                table: "AbpTenants",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserClaims_UserId",
                table: "AbpUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserLogins_LoginProvider_ProviderKey",
                table: "AbpUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId",
                table: "AbpUserOrganizationUnits",
                columns: new[] { "UserId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserRoles_RoleId_UserId",
                table: "AbpUserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_Email",
                table: "AbpUsers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_NormalizedEmail",
                table: "AbpUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_NormalizedUserName",
                table: "AbpUsers",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_UserName",
                table: "AbpUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Calibrations_CreatorId",
                table: "Lims_Calibrations",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Calibrations_EquipmentId",
                table: "Lims_Calibrations",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Calibrations_LastModifierId",
                table: "Lims_Calibrations",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Customers_CreatorId",
                table: "Lims_Customers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Customers_LastModifierId",
                table: "Lims_Customers",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicEquipmentTypes_CreatorId",
                table: "Lims_DicEquipmentTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicEquipmentTypes_LastModifierId",
                table: "Lims_DicEquipmentTypes",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicProductTypes_CreatorId",
                table: "Lims_DicProductTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicProductTypes_LastModifierId",
                table: "Lims_DicProductTypes",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicRatingTypes_CreatorId",
                table: "Lims_DicRatingTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicRatingTypes_LastModifierId",
                table: "Lims_DicRatingTypes",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicSampleProperties_CreatorId",
                table: "Lims_DicSampleProperties",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicSampleProperties_LastModifierId",
                table: "Lims_DicSampleProperties",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicSampleTypes_CreatorId",
                table: "Lims_DicSampleTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicSampleTypes_LastModifierId",
                table: "Lims_DicSampleTypes",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicStandardTypes_CreatorId",
                table: "Lims_DicStandardTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_DicStandardTypes_LastModifierId",
                table: "Lims_DicStandardTypes",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Equipment_CreatorId",
                table: "Lims_Equipment",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Equipment_DicEquipmentTypeId",
                table: "Lims_Equipment",
                column: "DicEquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Equipment_LastModifierId",
                table: "Lims_Equipment",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InspectionItems_CreatorId",
                table: "Lims_InspectionItems",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InspectionItems_DefaultEquipmentId",
                table: "Lims_InspectionItems",
                column: "DefaultEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InspectionItems_LastModifierId",
                table: "Lims_InspectionItems",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InspectionTasks_CreatorId",
                table: "Lims_InspectionTasks",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InspectionTasks_EquipmentId",
                table: "Lims_InspectionTasks",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InspectionTasks_LastModifierId",
                table: "Lims_InspectionTasks",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InspectionTasks_RecordDetailId",
                table: "Lims_InspectionTasks",
                column: "RecordDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Inventories_LocationId",
                table: "Lims_Inventories",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Inventories_ProductId",
                table: "Lims_Inventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryLogs_CreatorId",
                table: "Lims_InventoryLogs",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryLogs_LastModifierId",
                table: "Lims_InventoryLogs",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryLogs_LocationId",
                table: "Lims_InventoryLogs",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryLogs_ProductId",
                table: "Lims_InventoryLogs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryOutDetails_CreatorId",
                table: "Lims_InventoryOutDetails",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryOutDetails_InventoryOutId",
                table: "Lims_InventoryOutDetails",
                column: "InventoryOutId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryOutDetails_LastModifierId",
                table: "Lims_InventoryOutDetails",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryOutDetails_LocationId",
                table: "Lims_InventoryOutDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryOutDetails_ProductId",
                table: "Lims_InventoryOutDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryOuts_CreatorId",
                table: "Lims_InventoryOuts",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryOuts_LastModifierId",
                table: "Lims_InventoryOuts",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryStoreDetails_CreatorId",
                table: "Lims_InventoryStoreDetails",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryStoreDetails_InventoryStoreId",
                table: "Lims_InventoryStoreDetails",
                column: "InventoryStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryStoreDetails_LastModifierId",
                table: "Lims_InventoryStoreDetails",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryStoreDetails_LocationId",
                table: "Lims_InventoryStoreDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryStoreDetails_ProductId",
                table: "Lims_InventoryStoreDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryStores_CreatorId",
                table: "Lims_InventoryStores",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_InventoryStores_LastModifierId",
                table: "Lims_InventoryStores",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Locations_CreatorId",
                table: "Lims_Locations",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Locations_LastModifierId",
                table: "Lims_Locations",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Locations_WarehouseId",
                table: "Lims_Locations",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Maintenances_CreatorId",
                table: "Lims_Maintenances",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Maintenances_EquipmentId",
                table: "Lims_Maintenances",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Maintenances_LastModifierId",
                table: "Lims_Maintenances",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Products_CreatorId",
                table: "Lims_Products",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Products_DicProductTypeId",
                table: "Lims_Products",
                column: "DicProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Products_LastModifierId",
                table: "Lims_Products",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Products_StandardId",
                table: "Lims_Products",
                column: "StandardId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_RecordDetails_CreatorId",
                table: "Lims_RecordDetails",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_RecordDetails_InspectionItemId",
                table: "Lims_RecordDetails",
                column: "InspectionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_RecordDetails_LastModifierId",
                table: "Lims_RecordDetails",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_RecordDetails_RecordId",
                table: "Lims_RecordDetails",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Records_CreatorId",
                table: "Lims_Records",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Records_DicRatingTypeId",
                table: "Lims_Records",
                column: "DicRatingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Records_LastModifierId",
                table: "Lims_Records",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Records_SampleId",
                table: "Lims_Records",
                column: "SampleId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Repairs_CreatorId",
                table: "Lims_Repairs",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Repairs_EquipmentId",
                table: "Lims_Repairs",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Repairs_LastModifierId",
                table: "Lims_Repairs",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Samples_CreatorId",
                table: "Lims_Samples",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Samples_CustomerId",
                table: "Lims_Samples",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Samples_DicSamplePropertyId",
                table: "Lims_Samples",
                column: "DicSamplePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Samples_DicSampleTypeId",
                table: "Lims_Samples",
                column: "DicSampleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Samples_LastModifierId",
                table: "Lims_Samples",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Samples_ProductId",
                table: "Lims_Samples",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Samples_SupplierId",
                table: "Lims_Samples",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_StandardDetails_CreatorId",
                table: "Lims_StandardDetails",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_StandardDetails_InspectionItemId",
                table: "Lims_StandardDetails",
                column: "InspectionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_StandardDetails_LastModifierId",
                table: "Lims_StandardDetails",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_StandardDetails_StandardId",
                table: "Lims_StandardDetails",
                column: "StandardId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Standards_CreatorId",
                table: "Lims_Standards",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Standards_DicStandardTypeId",
                table: "Lims_Standards",
                column: "DicStandardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Standards_LastModifierId",
                table: "Lims_Standards",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Suppliers_CreatorId",
                table: "Lims_Suppliers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Suppliers_LastModifierId",
                table: "Lims_Suppliers",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_UsageHistories_CreatorId",
                table: "Lims_UsageHistories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_UsageHistories_EquipmentId",
                table: "Lims_UsageHistories",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_UsageHistories_LastModifierId",
                table: "Lims_UsageHistories",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Warehouses_CreatorId",
                table: "Lims_Warehouses",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lims_Warehouses_LastModifierId",
                table: "Lims_Warehouses",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
                table: "OpenIddictAuthorizations",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
                table: "OpenIddictTokens",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ReferenceId",
                table: "OpenIddictTokens",
                column: "ReferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpAuditLogActions");

            migrationBuilder.DropTable(
                name: "AbpBackgroundJobs");

            migrationBuilder.DropTable(
                name: "AbpClaimTypes");

            migrationBuilder.DropTable(
                name: "AbpEntityPropertyChanges");

            migrationBuilder.DropTable(
                name: "AbpFeatureGroups");

            migrationBuilder.DropTable(
                name: "AbpFeatures");

            migrationBuilder.DropTable(
                name: "AbpFeatureValues");

            migrationBuilder.DropTable(
                name: "AbpLinkUsers");

            migrationBuilder.DropTable(
                name: "AbpOrganizationUnitRoles");

            migrationBuilder.DropTable(
                name: "AbpPermissionGrants");

            migrationBuilder.DropTable(
                name: "AbpPermissionGroups");

            migrationBuilder.DropTable(
                name: "AbpPermissions");

            migrationBuilder.DropTable(
                name: "AbpRoleClaims");

            migrationBuilder.DropTable(
                name: "AbpSecurityLogs");

            migrationBuilder.DropTable(
                name: "AbpSettingDefinitions");

            migrationBuilder.DropTable(
                name: "AbpSettings");

            migrationBuilder.DropTable(
                name: "AbpTenantConnectionStrings");

            migrationBuilder.DropTable(
                name: "AbpUserClaims");

            migrationBuilder.DropTable(
                name: "AbpUserDelegations");

            migrationBuilder.DropTable(
                name: "AbpUserLogins");

            migrationBuilder.DropTable(
                name: "AbpUserOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpUserRoles");

            migrationBuilder.DropTable(
                name: "AbpUserTokens");

            migrationBuilder.DropTable(
                name: "Lims_Calibrations");

            migrationBuilder.DropTable(
                name: "Lims_InspectionTasks");

            migrationBuilder.DropTable(
                name: "Lims_Inventories");

            migrationBuilder.DropTable(
                name: "Lims_InventoryLogs");

            migrationBuilder.DropTable(
                name: "Lims_InventoryOutDetails");

            migrationBuilder.DropTable(
                name: "Lims_InventoryStoreDetails");

            migrationBuilder.DropTable(
                name: "Lims_Maintenances");

            migrationBuilder.DropTable(
                name: "Lims_Repairs");

            migrationBuilder.DropTable(
                name: "Lims_StandardDetails");

            migrationBuilder.DropTable(
                name: "Lims_UsageHistories");

            migrationBuilder.DropTable(
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "AbpEntityChanges");

            migrationBuilder.DropTable(
                name: "AbpTenants");

            migrationBuilder.DropTable(
                name: "AbpOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpRoles");

            migrationBuilder.DropTable(
                name: "Lims_RecordDetails");

            migrationBuilder.DropTable(
                name: "Lims_InventoryOuts");

            migrationBuilder.DropTable(
                name: "Lims_InventoryStores");

            migrationBuilder.DropTable(
                name: "Lims_Locations");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "AbpAuditLogs");

            migrationBuilder.DropTable(
                name: "Lims_InspectionItems");

            migrationBuilder.DropTable(
                name: "Lims_Records");

            migrationBuilder.DropTable(
                name: "Lims_Warehouses");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications");

            migrationBuilder.DropTable(
                name: "Lims_Equipment");

            migrationBuilder.DropTable(
                name: "Lims_DicRatingTypes");

            migrationBuilder.DropTable(
                name: "Lims_Samples");

            migrationBuilder.DropTable(
                name: "Lims_DicEquipmentTypes");

            migrationBuilder.DropTable(
                name: "Lims_Customers");

            migrationBuilder.DropTable(
                name: "Lims_DicSampleProperties");

            migrationBuilder.DropTable(
                name: "Lims_DicSampleTypes");

            migrationBuilder.DropTable(
                name: "Lims_Products");

            migrationBuilder.DropTable(
                name: "Lims_Suppliers");

            migrationBuilder.DropTable(
                name: "Lims_DicProductTypes");

            migrationBuilder.DropTable(
                name: "Lims_Standards");

            migrationBuilder.DropTable(
                name: "Lims_DicStandardTypes");

            migrationBuilder.DropTable(
                name: "AbpUsers");
        }
    }
}
