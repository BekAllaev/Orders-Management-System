using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Root.Views;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;
using Banner.Views;
using Banner;

namespace Root.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IRegionManager regionManager;
        IUnityContainer unityContainer;

        public MainWindowViewModel(IUnityContainer unityContainer,IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }

        public void ConfigureModuleCatalog()
        {
            regionManager.Regions["GlobalRegion"].Add(unityContainer.Resolve<ContentView>());
            regionManager.Regions["BannerRegion"].Add(unityContainer.Resolve<BannerView>());
        }
    }
}
