using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InspectionItems;
using Lanpuda.Lims.InspectionItems.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.InspectionMethods.InspectionItems.Lookups
{
    public class InspectionItemMultipleLookupViewModel : PagedViewModelBase<InspectionItemDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly IInspectionItemAppService _productAppService;

        public Action<ICollection<InspectionItemDto>>? OnSelectedCallback;


        /// <summary>
        /// 右侧表格数据
        /// </summary>
        public ObservableCollection<InspectionItemDto> SelectedInspectionItemList { get; set; }
        /// <summary>
        /// 右侧表格选中的product
        /// </summary>
        public InspectionItemDto? SelectedInspectionItem
        {
            get { return GetProperty(() => SelectedInspectionItem); }
            set { SetProperty(() => SelectedInspectionItem, value); }
        }

        #region 搜索
        public string? ShortName
        {
            get { return GetProperty(() => ShortName); }
            set { SetProperty(() => ShortName, value); }
        }


        public string? FullName
        {
            get { return GetProperty(() => FullName); }
            set { SetProperty(() => FullName, value); }
        }

        public string? Basis
        {
            get { return GetProperty(() => Basis); }
            set { SetProperty(() => Basis, value); }
        }


        public string? Unit
        {
            get { return GetProperty(() => Unit); }
            set { SetProperty(() => Unit, value); }
        }
        #endregion

        public InspectionItemMultipleLookupViewModel(IServiceProvider serviceProvider, IInspectionItemAppService productAppService)
        {
            _serviceProvider = serviceProvider;
            _productAppService = productAppService;
            this.SelectedInspectionItemList = new ObservableCollection<InspectionItemDto>();
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }



        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                InspectionItemGetListInput input = new InspectionItemGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.FullName = this.FullName;
                input.ShortName = this.ShortName;
                input.Basis = this.Basis;
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
            this.FullName = null;
            this.ShortName = null;
            this.Basis = null;
            this.Unit = null;
            await QueryAsync();
        }


        [Command]
        public void Select()
        {
            if (this.SelectedModel != null)
            {
                //判断是否已经添加
                var product = SelectedInspectionItemList.Where(m => m.Id == SelectedModel.Id).FirstOrDefault();
                if (product != null)
                {
                    this.SelectedInspectionItem = product;
                    Growl.Info("已经添加了");
                }
                else
                {
                    this.SelectedInspectionItemList.Add(SelectedModel);
                }
            }
        }

        [Command]
        public void SelectAll()
        {
            SelectedInspectionItemList.Clear();
            foreach (var item in PagedDatas)
            {
                SelectedInspectionItemList.Add(item);
            }
        }

        [Command]
        public void Delete()
        {
            if (this.SelectedInspectionItem != null)
            {
                this.SelectedInspectionItemList.Remove(SelectedInspectionItem);
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
                OnSelectedCallback(this.SelectedInspectionItemList);
                this.Close();
            }
        }
    }
}
