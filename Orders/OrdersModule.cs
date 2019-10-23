using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Orders.Views;

namespace Orders
{
    public class OrdersModule : IModule
    {
        IRegionManager regionManager;

        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RequestNavigate("OrdersRegion", "OrdersDashBoardView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<OrdersDashBoardView>();
        }
    }
}
