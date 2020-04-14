using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;
using Notification.Main;

namespace Notification
{
    public class NotificationModule : IModule
    {
        private IUnityContainer unityContainer;
        private IRegionManager regionManager;

        public NotificationModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            unityContainer.RegisterTypeForNavigation<NotificationMainView>();

            regionManager.RequestNavigate("NotificationRegion", "NotificationMainView");
        }
    }
}
