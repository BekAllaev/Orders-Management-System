using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using Settings.Main;
using Settings.Views;

namespace Settings
{
    public class SettingsModule : IModule
    {
        IUnityContainer unityContainer;
        IRegionManager regionManager;

        public SettingsModule(IUnityContainer unityContainer,IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            unityContainer.RegisterTypeForNavigation<SettingsMainView>();

            regionManager.RegisterViewWithRegion("OrdersSettingsRegion", typeof(OrdersSettingView));

            regionManager.RequestNavigate("SettingRegion", "SettingsMainView");
        }
    }
}
