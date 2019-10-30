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

namespace Root
{
    class Bootstrapper : UnityBootstrapper
    {
        //MainWindowViewModel viewModel;
        
        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            moduleCatalog.AddModule(typeof(DashboardModule));
            moduleCatalog.AddModule(typeof(BannerModule));
            moduleCatalog.AddModule(typeof(OrdersModule));
        }

        protected override DependencyObject CreateShell()
        {
            //viewModel = Container.Resolve<MainWindowViewModel>();
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
