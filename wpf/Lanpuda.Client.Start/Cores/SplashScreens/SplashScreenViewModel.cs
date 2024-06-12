using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Account.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Entities;
using Lanpuda.Client.Theme.Services.MenuServices;
using Lanpuda.Client.Theme.Services.SettingsServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lanpuda.Client.Start.Cores.SplashScreens
{
    public class SplashScreenViewModel : RootViewModelBase
    {
        private readonly IMenuService _menuService;
        private readonly ISettingsService _settingsService;

        public SplashScreenViewModel(
            IMenuService menuService,
            ISettingsService settingsService)
        {
            _menuService = menuService;
            _settingsService = settingsService;
        }


        [Command]
        public async Task Loaded()
        {
            //string path = "app-data.json";
            //StreamReader sr = File.OpenText(path);
            try
            {
                this.IsLoading = true;
                //for (int i = 0; i <= 1; i++)
                //{
                //    //Progress = i;
                //    await Task.Delay(1);
                //}
                await Task.Delay(2000);
                //string jsonAppData = sr.ReadToEnd();
                //var appData = JsonConvert.DeserializeObject<AppData>(jsonAppData);
                //if (appData != null)
                //{
                //    _settingsService.SetAppName(appData.AppDescription.Name);
                //    _settingsService.SetAppDescription(appData.AppDescription.Description);
                //    _settingsService.SetUserAvatar(appData.UserDescription.Avatar);
                //    _settingsService.SetUserName(appData.UserDescription.Name);
                //    _settingsService.SetUserEmail(appData.UserDescription.Email);
                //}

                ModuleManager.DefaultManager.Clear(RegionNames.MainWindow);

                ModuleManager.DefaultManager.Inject(RegionNames.MainWindow, AccountUIViewKeys.Login);

                ModuleManager.DefaultManager.Inject(RegionNames.MainWindow, ClientStartViewKeys.DefaultLayout);
                ModuleManager.DefaultManager.Inject(RegionNames.MenuTreeRegion, ClientStartViewKeys.Menu);
                ModuleManager.DefaultManager.Inject(RegionNames.MainHeaderRegion, ClientStartViewKeys.Header);

                ModuleManager.DefaultManager.Navigate(RegionNames.MainWindow, AccountUIViewKeys.Login);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally
            {
                this.IsLoading = false;
                //sr.Dispose();
            }
        }

    }
}
