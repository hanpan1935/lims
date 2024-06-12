using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.Records.BatchCreates
{
    public class RecordBatchCreateViewModel : EditViewModelBase<RecordBatchCreateModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get;  set; }
    }
}
