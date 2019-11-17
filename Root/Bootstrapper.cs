using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Modularity;
using Orders;
using Root.Views;
using Root.ViewModels;
using Microsoft.Practices.Unity;
using System.Reflection;
using Dashboard;
using Banner;
using Root.Settings;

namespace Root
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            moduleCatalog.AddModule(typeof(DashboardModule));
            moduleCatalog.AddModule(typeof(BannerModule));
            moduleCatalog.AddModule(typeof(OrdersModule));
        }

        protected override DependencyObject CreateShell()
        {
            Container.RegisterTypeForNavigation<SettingsView>();

            //Because of AutoWire, ViewModel of MainWindow is resolved too in this step. So we can use MainWindowViewModel
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            var viewModel = (MainWindowViewModel)Application.Current.MainWindow.DataContext;
            viewModel.ConfigureModuleCatalog();

            Application.Current.MainWindow.Show();
        }
    }
}
