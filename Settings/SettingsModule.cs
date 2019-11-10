using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
//using Settings.Main;
using Settings.OrdersSettings;

namespace Settings
{
    public class SettingsModule : IModule
    {
        IUnityContainer unityContainer;


        public SettingsModule(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
            unityContainer.RegisterInstance(Properties.Settings.Default);
        }

        public void Initialize()
        {
            //unityContainer.RegisterTypeForNavigation<SettingsMainView>();
            unityContainer.RegisterTypeForNavigation<MainViewSettingView>();
        }
    }
}
