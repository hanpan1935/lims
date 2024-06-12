using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Products.Dtos;
using Lanpuda.Lims.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using HandyControl.Controls;
using HandyControl.Collections;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;

namespace Lanpuda.Lims.UI.BasicInformations.Products.Lookups
{
    public class ProductMultipleLookupViewModel : PagedViewModelBase<ProductDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly IProductAppService _productAppService;

        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        public ManualObservableCollection<DicProductTypeLookupDto> ProductTypeSource { get; set; }


        public Action<ICollection<ProductDto>>? OnSelectedCallback;

        /// <summary>
        /// 右侧表格数据
        /// </summary>
        public ObservableCollection<ProductDto> SelectedProductList { get; set; }
        /// <summary>
        /// 右侧表格选中的product
        /// </summary>
        public ProductDto? SelectedProduct
        {
            get { return GetProperty(() => SelectedProduct); }
            set { SetProperty(() => SelectedProduct, value); }
        }

        #region 搜索
        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        public string Unit
        {
            get { return GetProperty(() => Unit); }
            set { SetProperty(() => Unit, value); }
        }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public int? DicProductTypeId
        {
            get { return GetProperty(() => DicProductTypeId); }
            set { SetProperty(() => DicProductTypeId, value); }
        }


        public string Spec
        {
            get { return GetProperty(() => Spec); }
            set { SetProperty(() => Spec, value); }
        }

        #endregion

        public ProductMultipleLookupViewModel(IServiceProvider serviceProvider, IProductAppService productAppService, IDataDictionaryAppService dataDictionaryAppService)
        {
            this.PageTitle = "产品物料";
            _serviceProvider = serviceProvider;
            _productAppService = productAppService;
            this.SelectedProductList = new ObservableCollection<ProductDto>();
            _dataDictionaryAppService = dataDictionaryAppService;
            this.ProductTypeSource = new ManualObservableCollection<DicProductTypeLookupDto>();
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            this.IsLoading = true;
            var productTypeList = await _dataDictionaryAppService.LookupProductTypeAsync();
            this.ProductTypeSource.Clear();
            foreach (var item in productTypeList)
            {
                this.ProductTypeSource.Add(item);
            }
            await this.QueryAsync();
        }



        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                ProductGetListInput input = new ProductGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Number = this.Number;
                input.Name = this.Name;
                input.Spec = this.Spec;
                input.DicProductTypeId = this.DicProductTypeId;
                input.Unit = this.Unit;
                var result = await _productAppService.GetPagedListAsync(input);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                this.PagedDatas.CanNotify = true;
            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = string.Empty;
            this.Name = string.Empty;
            this.Spec = string.Empty;
            this.DicProductTypeId = null;
            this.Unit = string.Empty;
            await QueryAsync();
        }


        [Command]
        public void Select()
        {
            if (this.SelectedModel != null)
            {
                //判断是否已经添加
                var product = SelectedProductList.Where(m => m.Id == SelectedModel.Id).FirstOrDefault();
                if (product != null)
                {
                    this.SelectedProduct = product;
                    Growl.Info("已经添加了");
                }
                else
                {
                    this.SelectedProductList.Add(SelectedModel);
                }
            }
        }


        [Command]
        public void Delete()
        {
            if (this.SelectedProduct != null)
            {
                this.SelectedProductList.Remove(SelectedProduct);
            }
        }


        [Command]
        public void Close()
        {
            if (CurrentWindowService != null)
                CurrentWindowService.Close();
        }


        [Command]
        public void Save()
        {
            if (this.OnSelectedCallback != null)
            {
                OnSelectedCallback(this.SelectedProductList);
                this.Close();
            }
        }
    }
}
