using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using Banner.Main;
using Banner.Views;

namespace Banner
{
    public class BannerModule : IModule
    {
        private IUnityContainer unityContainer;
        private IRegionManager regionManager;

        public BannerModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            unityContainer.RegisterTypeForNavigation<BannerMainView>();
            unityContainer.RegisterTypeForNavigation<NotificationView>();

            regionManager.RequestNavigate("BannerRegion", "BannerMainView");
            regionManager.RequestNavigate("NotificationRegion", "NotificationView");
        }
    }
}
