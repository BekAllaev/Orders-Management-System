using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using OMS.WPFClient.Modules.Settings.Main;
using OMS.WPFClient.Modules.Settings.Views;

namespace OMS.WPFClient.Modules.Settings
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
            unityContainer.RegisterTypeForNavigation<GeneralSettingView>();

            regionManager.RegisterViewWithRegion("GeneralSettingsRegion", typeof(GeneralSettingView));
            regionManager.RegisterViewWithRegion("OrdersSettingsRegion", typeof(OrdersSettingView));

            regionManager.RequestNavigate("SettingRegion", "SettingsMainView");
        }
    }
}
