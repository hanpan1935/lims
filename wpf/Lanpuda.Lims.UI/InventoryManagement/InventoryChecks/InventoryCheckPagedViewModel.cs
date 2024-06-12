using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.InventoryManagement.InventoryChecks
{
    public class InventoryCheckPagedViewModel : PagedViewModelBase<InventoryCheckPagedModel>
    {
        public InventoryCheckPagedViewModel() { }


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
