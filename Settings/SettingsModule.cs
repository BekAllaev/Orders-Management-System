using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using Settings.MainView;
using Settings.OrdersSettings;

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
            unityContainer.RegisterTypeForNavigation<SettingsPanelView>();

            regionManager.RegisterViewWithRegion("OrdersSettingsRegion", typeof(OrdersSettingView));

            regionManager.RequestNavigate("SettingRegion", "SettingsPanelView");
        }
    }
}
