using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Products.Dtos;
using Lanpuda.Lims.Products;
using Lanpuda.Lims.UI.BasicInformations.Products.Edits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using Lanpuda.Lims.DataDictionaries;
using HandyControl.Collections;
using Lanpuda.Lims.DataDictionaries.Dtos;

namespace Lanpuda.Lims.UI.BasicInformations.Products.Lookups
{
    public class ProductSingleLookupViewModel : PagedViewModelBase<ProductDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly IProductAppService _productAppService;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        public ManualObservableCollection<DicProductTypeLookupDto> ProductTypeSource { get; set; }

        public Action<ProductDto>? OnSelectedCallback;

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

        public ProductSingleLookupViewModel(IServiceProvider serviceProvider, IProductAppService productAppService, IDataDictionaryAppService dataDictionaryAppService)
        {
            _serviceProvider = serviceProvider;
            _productAppService = productAppService;
            _dataDictionaryAppService = dataDictionaryAppService;
            this.ProductTypeSource = new ManualObservableCollection<DicProductTypeLookupDto>();
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
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
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
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
                ProductGetListInput input = new ProductGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Number = this.Number;
                input.Name = this.Name;
                input.Unit = this.Unit;
                input.Spec = this.Spec;
                input.DicProductTypeId = this.DicProductTypeId;
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


        [Command]
        public void OnSelected()
        {
            if (this.OnSelectedCallback != null && this.SelectedModel != null)
            {
                OnSelectedCallback(this.SelectedModel);
                CurrentWindowService.Close();
            }
        }

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = string.Empty;
            this.Name = string.Empty;
            this.Unit = string.Empty;
            this.Spec = string.Empty;
            this.DicProductTypeId = null;
            await QueryAsync();
        }
    }
}
