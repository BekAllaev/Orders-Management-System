using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Unity;
using Prism.Regions;
using Orders.Views;
using Microsoft.Practices.Unity;

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
            unityContainer.RegisterTypeForNavigation<OrdersDashboardView>();

            regionManager.RequestNavigate("OrdersRegion", "OrdersDashboardView");
        }

        //public void OnInitialized(IContainerProvider containerProvider)
        //{
        //    regionManager = containerProvider.Resolve<IRegionManager>();

        //    regionManager.RequestNavigate("OrdersRegion", "OrdersDashBoardView");
        //}

        //public void RegisterTypes(IContainerRegistry containerRegistry)
        //{
        //    containerRegistry.RegisterForNavigation<OrdersDashBoardView>();
        //}
    }
}
