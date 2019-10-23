using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Modularity;
using Orders;
using System.Reflection;

namespace Root
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            //var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            //moduleCatalog.AddModule(typeof(OrdersModule));

            Type ordersModuleType = typeof(OrdersModule);
            ModuleCatalog.AddModule(
            new ModuleInfo()
            {
                ModuleName = ordersModuleType.Name,
                ModuleType = ordersModuleType.AssemblyQualifiedName,
            });
        }
    }
}
