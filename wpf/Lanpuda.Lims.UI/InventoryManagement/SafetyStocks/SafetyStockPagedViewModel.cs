using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.InventoryManagement.SafetyStocks
{
    public class SafetyStockPagedViewModel : PagedViewModelBase<SafetyStockPagedModel>
    {

        public SafetyStockPagedViewModel()
        {
            this.PageTitle = "安全库存";
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }



        protected override async Task GetPagedDatasAsync()
        {
            await Task.CompletedTask;
        }
    }
}
