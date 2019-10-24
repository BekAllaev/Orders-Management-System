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
    }
}
