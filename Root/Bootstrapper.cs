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
using Prism.Regions;
using Syncfusion.Windows.Tools.Controls;
using Infrastructure;
using Settings;
using DataAccessLocal;
using System.Data.Entity;
using Infrastructure.Services;

namespace Root
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            moduleCatalog.AddModule(typeof(BannerModule));
            moduleCatalog.AddModule(typeof(SettingsModule));
            moduleCatalog.AddModule(typeof(DashboardModule));
            moduleCatalog.AddModule(typeof(OrdersModule));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterInstance(new NorthwindContext(), new TransientLifetimeManager());
            Container.RegisterInstance(new TitleUpdater());
        }

        protected override DependencyObject CreateShell()
        {
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
