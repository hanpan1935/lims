using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Start.Cores.SplashScreens;
using Lanpuda.Client.Start.Cores.Welcomes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Lanpuda.Client.Start
{
    public class ClientStartBootstraper
    {
        private IModuleManager _moduleManager;
        private readonly IServiceProvider _serviceProvider;

        public ClientStartBootstraper(IModuleManager moduleManager, IServiceProvider serviceProvider)
        {
            _moduleManager = moduleManager;
            _serviceProvider = serviceProvider;
        }

        public void Load()
        {
            var splashScreen = new DevExpress.Mvvm.ModuleInjection.Module(
               ClientStartViewKeys.SplashScreen,
               () =>
               {
                   var viewModel = _serviceProvider.GetService<Cores.SplashScreens.SplashScreenViewModel>();
                   return viewModel;
               },
               typeof(SplashScreenView)
               );

            var welcome = new DevExpress.Mvvm.ModuleInjection.Module(
              ClientStartViewKeys.Welcome,
              () =>
              {
                  var viewModel = _serviceProvider.GetService<WelcomeViewModel>();
                  return viewModel;
              },
              typeof(WelcomeView)
              );

            _moduleManager.Register(RegionNames.MainWindow, splashScreen);
            _moduleManager.Register(RegionNames.MainContentRegion, welcome);


            //var viewLocator = new ViewLocator(App.Current);
            //ViewLocator.Default = viewLocator;




        }
    }
}
