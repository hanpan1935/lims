using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Lims.Equipments;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.EquipmentManagement.Equipments.Lookups
{
    public class EquipmentMultipleLookupViewModel : PagedViewModelBase<EquipmentDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly IEquipmentAppService _equipmentAppService;

        public Action<ICollection<EquipmentDto>>? OnSelectedCallback;


        /// <summary>
        /// 右侧表格数据
        /// </summary>
        public ObservableCollection<EquipmentDto> SelectedEquipmentList { get; set; }
        /// <summary>
        /// 右侧表格选中的equipment
        /// </summary>
        public EquipmentDto? SelectedEquipment
        {
            get { return GetProperty(() => SelectedEquipment); }
            set { SetProperty(() => SelectedEquipment, value); }
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
       
        #endregion

        public EquipmentMultipleLookupViewModel(IServiceProvider serviceProvider, IEquipmentAppService equipmentAppService)
        {
            this.PageTitle = "选择设备";
            _serviceProvider = serviceProvider;
            _equipmentAppService = equipmentAppService;
            this.SelectedEquipmentList = new ObservableCollection<EquipmentDto>();
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
                EquipmentGetListInput input = new EquipmentGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                //input.Number = this.Number;
                var result = await _equipmentAppService.GetPagedListAsync(input);
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
            this.Name = string.Empty;
            await QueryAsync();
        }


        [Command]
        public void Select()
        {
            if (this.SelectedModel != null)
            {
                //判断是否已经添加
                var equipment = SelectedEquipmentList.Where(m => m.Id == SelectedModel.Id).FirstOrDefault();
                if (equipment != null)
                {
                    this.SelectedEquipment = equipment;
                    Growl.Info("已经添加了");
                }
                else
                {
                    this.SelectedEquipmentList.Add(SelectedModel);
                }
            }
        }


        [Command]
        public void Delete()
        {
            if (this.SelectedEquipment != null)
            {
                this.SelectedEquipmentList.Remove(SelectedEquipment);
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
                OnSelectedCallback(this.SelectedEquipmentList);
                this.Close();
            }
        }
    }
}
