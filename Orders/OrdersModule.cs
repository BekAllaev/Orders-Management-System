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
        IUnityContainer unityContainer;
        IRegionManager regionManager;

        public OrdersModule(IUnityContainer unityContainer,IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            unityContainer.RegisterTypeForNavigation<OrdersMainView>();
            unityContainer.RegisterTypeForNavigation<OrdersSettingView>();
            unityContainer.RegisterTypeForNavigation<CreateView>();
            unityContainer.RegisterTypeForNavigation<JournalView>();

            regionManager.RegisterViewWithRegion("OrdersSettingsRegion", typeof(OrdersSettingView));
        }
    }
}
