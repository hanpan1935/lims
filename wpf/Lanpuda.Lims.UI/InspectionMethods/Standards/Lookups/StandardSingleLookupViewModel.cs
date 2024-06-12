using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Standards.Dtos;
using Lanpuda.Lims.Standards;
using Lanpuda.Lims.UI.InspectionMethods.Standards.Edits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.Products.Dtos;
using DevExpress.Mvvm;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;
using System.Collections.ObjectModel;

namespace Lanpuda.Lims.UI.InspectionMethods.Standards.Lookups
{
    public class StandardSingleLookupViewModel : PagedViewModelBase<StandardDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly IStandardAppService _standardAppService;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        public ObservableCollection<DicStandardTypeLookupDto> StandardTypeSource { get; set; }

        public Action<StandardDto>? OnSelectedCallback;
        public StandardSingleLookupViewModel(
            IServiceProvider serviceProvider, 
            IStandardAppService standardAppService,
            IDataDictionaryAppService dataDictionaryAppService)
        {
            this.PageTitle = "检验标准";
            this._serviceProvider = serviceProvider;
            _standardAppService = standardAppService;
            _dataDictionaryAppService = dataDictionaryAppService;
            StandardTypeSource = new ObservableCollection<DicStandardTypeLookupDto>();
        }

        #region search
        public string? Description
        {
            get { return GetProperty(() => Description); }
            set { SetProperty(() => Description, value); }
        }

        public DicStandardTypeLookupDto? StandardType
        {
            get { return GetProperty(() => StandardType); }
            set { SetProperty(() => StandardType, value); }
        }

        #endregion

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                var standardTypeList = await _dataDictionaryAppService.LookupStandardTypeAsync();
                StandardTypeSource.Clear();
                foreach (var item in standardTypeList)
                {
                    StandardTypeSource.Add(item);
                }

                await this.QueryAsync();
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
            this.Description = null;
            this.StandardType = null;
            await QueryAsync();
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                StandardGetListInput input = new StandardGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Description = this.Description;
                if (this.StandardType != null)
                {
                    input.DicStandardTypeId = StandardType.Id;
                }
                var result = await _standardAppService.GetPagedListAsync(input);
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

    }
}
