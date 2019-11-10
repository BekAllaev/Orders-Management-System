using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Unity;
using Prism.Regions;
using Orders.Views;
using Orders.Main;
using Microsoft.Practices.Unity;
using System.Configuration;
using Orders.Settings;

namespace Orders
{
    public class OrdersModule : IModule
    {
        IRegionManager regionManager;
        IUnityContainer unityContainer;

        public OrdersModule(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;

            regionManager = unityContainer.Resolve<IRegionManager>();
        }

        public void Initialize()
        {
            unityContainer.RegisterTypeForNavigation<OrdersMainView>();
            unityContainer.RegisterTypeForNavigation<OrdersSettingView>();
            unityContainer.RegisterTypeForNavigation<CreateView>();
            unityContainer.RegisterTypeForNavigation<JournalView>();

            regionManager.RegisterViewWithRegion("OrdersManagmentRegion", Type.GetType("Orders.Views." + Properties.Settings.Default.OrdersMainView));
        }
    }
}
