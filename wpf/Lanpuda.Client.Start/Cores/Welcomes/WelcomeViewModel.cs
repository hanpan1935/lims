using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Lanpuda.Client.Start.Cores.Welcomes
{
    public class WelcomeViewModel : RootViewModelBase
    {
        public BitmapImage Avatar { get; set; }

        public string Employees
        {
            get { return GetProperty(() => Employees); }
            set { SetProperty(() => Employees, value); }
        }

        public WelcomeViewModel()
        {
            Avatar = new BitmapImage(new Uri("https://gw.alipayobjects.com/zos/rmsportal/lctvVCLfRpYCkYxAsiVQ.png"));
            this.PageTitle = "首页-工作台1";
        }

        [Command]
        public void Test()
        {
            this.Employees = DateTime.Now.ToString() + "BB";
        }
    }
}
