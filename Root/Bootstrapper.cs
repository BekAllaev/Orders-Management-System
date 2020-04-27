using Orders;
using Dashboard;
using Banner;
using Settings;
using DataAccessLocal;
using Notification;
using Infrastructure;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Modularity;
using Root.Views;
using Root.ViewModels;
using Microsoft.Practices.Unity;
using System.Reflection;
using Prism.Regions;
using Syncfusion.Windows.Tools.Controls;
using System.Data.Entity;
using Infrastructure.Services;
using Infrastructure.SettingsRepository;

namespace Root
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            moduleCatalog.AddModule(typeof(NotificationModule));
            moduleCatalog.AddModule(typeof(BannerModule));
            moduleCatalog.AddModule(typeof(SettingsModule));
            moduleCatalog.AddModule(typeof(DashboardModule));
            moduleCatalog.AddModule(typeof(OrdersModule));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterInstance<IUserSettingsRepository>(new UserSettingsRepository());
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
