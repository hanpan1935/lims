using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Records.Dtos;
using Lanpuda.Lims.Records;
using Lanpuda.Lims.UI.Records.BatchCreates;
using Lanpuda.Lims.UI.Records.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Lanpuda.Lims.UI.InspectionMethods.InspectionItems.Lookups;
using Lanpuda.Lims.InspectionItems.Dtos;
using System.Data;
using Lanpuda.Lims.InspectionItems;
using static Lanpuda.Lims.Permissions.LimsPermissions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;
using Lanpuda.Lims.UI.BasicInformations.Customers.Lookups;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Lanpuda.Lims.UI.BasicInformations.Suppliers.Lookups;
using HandyControl.Collections;

namespace Lanpuda.Lims.UI.Records.Items
{
    public class RecordItemsPagedViewModel : PagedViewModelBase<RecordDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRecordAppService _recordAppService;
        private readonly IInspectionItemAppService _inspectionItemAppService;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;

        public ObservableCollection<DicSampleTypeLookupDto> SampleTypeSource { get; set; }
        public ObservableCollection<DicSamplePropertyLookupDto> SamplePropertySource { get; set; }
        public ObservableCollection<DicRatingTypeLookupDto> RatingTypeSource { get; set; }


        public DataTable DataSource
        {
            get { return GetProperty(() => DataSource); }
            set { SetProperty(() => DataSource, value); }
        }

        public ObservableCollection<InspectionItemDto> InspectionItems
        {
            get { return GetProperty(() => InspectionItems); }
            set { SetProperty(() => InspectionItems, value); }
        }


        public DataRowView? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }


        #region search
        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public string? SampleNumber
        {
            get { return GetProperty(() => SampleNumber); }
            set { SetProperty(() => SampleNumber, value); }
        }


        public Guid? ProductId { get; set; }
        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public DicSampleTypeLookupDto? SelectSampleType
        {
            get { return GetProperty(() => SelectSampleType); }
            set { SetProperty(() => SelectSampleType, value); }
        }


        public DicSamplePropertyLookupDto? SelectSampleProperty
        {
            get { return GetProperty(() => SelectSampleProperty); }
            set { SetProperty(() => SelectSampleProperty, value); }
        }

        public DicRatingTypeLookupDto? SelectRatingType
        {
            get { return GetProperty(() => SelectRatingType); }
            set { SetProperty(() => SelectRatingType, value); }
        }


        public DateTime? SampleTimeStart
        {
            get { return GetProperty(() => SampleTimeStart); }
            set { SetProperty(() => SampleTimeStart, value); }
        }


        public DateTime? SampleTimeEnd
        {
            get { return GetProperty(() => SampleTimeEnd); }
            set { SetProperty(() => SampleTimeEnd, value); }
        }


        public string? Sender
        {
            get { return GetProperty(() => Sender); }
            set { SetProperty(() => Sender, value); }
        }


        public Guid? CustomerId { get; set; }
        public string? CustomerName
        {
            get { return GetProperty(() => CustomerName); }
            set { SetProperty(() => CustomerName, value); }
        }

        public Guid? SupplierId { get; set; }
        public string? SupplierName
        {
            get { return GetProperty(() => SupplierName); }
            set { SetProperty(() => SupplierName, value); }
        }



        #endregion


        public RecordItemsPagedViewModel(
            IServiceProvider serviceProvider,
            IRecordAppService recordAppService, 
            IInspectionItemAppService inspectionItemAppService,
            IDataDictionaryAppService dataDictionaryAppService)
        {
            this.PageTitle = "检验数据";
            _serviceProvider = serviceProvider;
            _recordAppService = recordAppService;
            _inspectionItemAppService = inspectionItemAppService;
            InspectionItems = new ObservableCollection<InspectionItemDto>();
            DataSource = new DataTable();
            SampleTypeSource = new ObservableCollection<DicSampleTypeLookupDto>();
            SamplePropertySource = new ObservableCollection<DicSamplePropertyLookupDto>();
            RatingTypeSource = new ObservableCollection<DicRatingTypeLookupDto>();
            _dataDictionaryAppService = dataDictionaryAppService;
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var itemsList = await _inspectionItemAppService.GetAllAsync();
                InspectionItems = new ObservableCollection<InspectionItemDto>(itemsList);

                var sampleTypes = await _dataDictionaryAppService.LookupSampleTypeAsync();
                SampleTypeSource.Clear();
                foreach (var item in sampleTypes)
                {
                    this.SampleTypeSource.Add(item);
                }
                var sampleProperties = await _dataDictionaryAppService.LookupSamplePropertyAsync();
                SamplePropertySource.Clear();
                foreach (var item in sampleProperties)
                {
                    this.SamplePropertySource.Add(item);
                }

                var ratingTypes = await _dataDictionaryAppService.LookupRatingTypeAsync();
                RatingTypeSource.Clear();
                foreach (var item in ratingTypes)
                {
                    this.RatingTypeSource.Add(item);
                }
                await this.QueryAsync();
                
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                RecordGetListInput input = new RecordGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Number = this.Number;
                input.SampleNumber = this.SampleNumber;
                input.ProductId = this.ProductId;
                input.DicSampleTypeId = this.SelectSampleType?.Id;
                input.DicSamplePropertyId = this.SelectSampleProperty?.Id;
                input.DicRatingTypeId = this.SelectRatingType?.Id;
                input.SampleTimeStart = this.SampleTimeStart;
                input.SampleTimeEnd = this.SampleTimeEnd;
                input.Sender = this.Sender;
                input.CustomerId = this.CustomerId;
                input.SupplierId = this.SupplierId;
                var result = await _recordAppService.GetPagedListAsync(input);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                this.PagedDatas.CanNotify = true;
                TransformToDataTable();
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = null;
            this.SampleNumber = null;
            this.ProductId = null;
            this.ProductName = null;
            this.SelectSampleType = null;
            this.SelectSampleProperty = null;
            this.SelectRatingType = null;
            this.SampleTimeStart = null;
            this.SampleTimeEnd = null;
            this.Sender = null;
            this.CustomerId = null;
            this.CustomerName = null;
            this.SupplierId = null;
            this.CustomerId = null;
            await QueryAsync();
        }

        [Command]
        public void Create()
        {
            if (this.WindowService != null)
            {
                RecordEditViewModel? viewModel = _serviceProvider.GetService<RecordEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "检验数据-新建";
                    WindowService.Show(nameof(RecordEditView), viewModel);
                }
            }
        }

        [Command]
        public void BathcCreate()
        {
            if (this.WindowService != null)
            {
                RecordBatchCreateViewModel? viewModel = _serviceProvider.GetService<RecordBatchCreateViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "检验数据-批量新建";
                    WindowService.Show(nameof(RecordBatchCreateView), viewModel);
                }
            }
        }

        

        [Command]
        public void Update()
        {
            if (this.SelectedRow == null)
            {
                return;
            }
            if (this.WindowService != null)
            {
                RecordEditViewModel? viewModel = _serviceProvider.GetService<RecordEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = (Guid?)this.SelectedRow.Row["Id"];
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "检验数据-编辑";
                    WindowService.Show(nameof(RecordEditView), viewModel);
                }
            }
        }


        [Command]
        public void ShowPagedView()
        {
            ModuleManager.DefaultManager.InjectOrNavigate(RegionNames.MainContentRegion, LimsUIViewKeys.Lims_Record, null);
        }


        [Command]
        public void ShowInspectionItemSelectView()
        {
            if (this.WindowService != null)
            {
                InspectionItemMultipleLookupViewModel? viewModel = _serviceProvider.GetService<InspectionItemMultipleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (items) =>
                    {
                        InspectionItems = new ObservableCollection<InspectionItemDto>(items);
                    };
                    WindowService.Title = "选择检验项目";
                    WindowService.Show(nameof(InspectionItemMultipleLookupView), viewModel);
                }
            }
        }


    



        private void TransformToDataTable()
        {
            var datas = this.PagedDatas;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id",typeof(Guid));
            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("DicRatingTypeId");
            dataTable.Columns.Add("DicRatingTypeDisplayValue");
            dataTable.Columns.Add("Remark");
            dataTable.Columns.Add("SampleId");
            dataTable.Columns.Add("SampleNumber");
            dataTable.Columns.Add("ProductName");
            dataTable.Columns.Add("DicSampleTypeId");
            dataTable.Columns.Add("DicSampleTypeDisplayValue");
            dataTable.Columns.Add("DicSamplePropertyId");
            dataTable.Columns.Add("DicSamplePropertyDisplayValue");
            dataTable.Columns.Add("SampleTime");
            dataTable.Columns.Add("ExpireTime");
            dataTable.Columns.Add("SampleCount");
            dataTable.Columns.Add("Sender");
            dataTable.Columns.Add("CustomerId");
            dataTable.Columns.Add("CustomerFullName");
            dataTable.Columns.Add("CustomerShortName");
            dataTable.Columns.Add("SupplierId");
            dataTable.Columns.Add("SupplierFullName");
            dataTable.Columns.Add("SupplierShortName");
            foreach (var item in InspectionItems)
            {
                dataTable.Columns.Add(item.Id.ToString());
                dataTable.Columns.Add(item.Id.ToString() + "IsQualified");
            }


            foreach (var data in datas)
            {
                DataRow dataRow = dataTable.Rows.Add();
                dataRow["Id"] = data.Id;
                dataRow["Number"] = data.Number;
                dataRow["DicRatingTypeId"] = data.DicRatingTypeId;
                dataRow["DicRatingTypeDisplayValue"] = data.DicRatingTypeDisplayValue;
                dataRow["Remark"] = data.Remark;
                dataRow["SampleId"] = data.SampleId;
                dataRow["SampleNumber"] = data.SampleNumber;
                dataRow["ProductName"] = data.ProductName;
                dataRow["DicSampleTypeId"] = data.DicSampleTypeId;
                dataRow["DicSampleTypeDisplayValue"] = data.DicSampleTypeDisplayValue;
                dataRow["DicSamplePropertyId"] = data.DicSamplePropertyId;
                dataRow["DicSamplePropertyDisplayValue"] = data.DicSamplePropertyDisplayValue;
                dataRow["SampleTime"] = data.SampleTime;
                dataRow["ExpireTime"] = data.ExpireTime;
                dataRow["SampleCount"] = data.SampleCount;
                dataRow["Sender"] = data.Sender;
                dataRow["CustomerId"] = data.CustomerId;
                dataRow["CustomerFullName"] = data.CustomerFullName;
                dataRow["CustomerShortName"] = data.CustomerShortName;
                dataRow["SupplierId"] = data.SupplierId;
                dataRow["SupplierFullName"] = data.SupplierFullName;
                dataRow["SupplierShortName"] = data.SupplierShortName;

                foreach (var detailDto in data.Details)
                {
                    dataRow[detailDto.InspectionItemId.ToString()] = detailDto.ResultValue;
                    dataRow[detailDto.InspectionItemId.ToString() + "IsQualified"] = detailDto.IsQualified;
                }

               
            }


            this.DataSource = dataTable;
        }


        [Command]
        public void ShowProductSelectView()
        {
            if (this.WindowService != null)
            {
                ProductSingleLookupViewModel? viewModel = _serviceProvider.GetService<ProductSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (product) =>
                    {
                        this.ProductId = product.Id;
                        this.ProductName = product.Name;
                    };
                    WindowService.Title = "选择产品";
                    WindowService.Show(nameof(ProductSingleLookupView), viewModel);
                }
            }
        }

        [Command]
        public void ShowCustomerSelectView()
        {
            if (this.WindowService != null)
            {
                CustomerSingleLookupViewModel? viewModel = _serviceProvider.GetService<CustomerSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (customer) =>
                    {
                        this.CustomerId = customer.Id;
                        this.CustomerName = customer.ShortName;
                    };
                    WindowService.Title = "选择客户";
                    WindowService.Show(nameof(CustomerSingleLookupView), viewModel);
                }
            }
        }

        [Command]
        public void ShowSupplierSelectView()
        {
            if (this.WindowService != null)
            {
                SupplierSingleLookupViewModel? viewModel = _serviceProvider.GetService<SupplierSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (supplier) =>
                    {
                        this.SupplierId = supplier.Id;
                        this.SupplierName = supplier.ShortName;
                    };
                    WindowService.Title = "选择供应商";
                    WindowService.Show(nameof(SupplierSingleLookupView), viewModel);
                }
            }
        }

    }
}
