using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Root.Views;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;
using Banner.Main;
using Banner;
using Infrastructure;
using Prism.Commands;

namespace Root.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Declarations
        IRegionManager regionManager;
        IUnityContainer unityContainer;
        #endregion

        #region Constructors
        public MainWindowViewModel(IUnityContainer unityContainer,IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }
        #endregion

        #region Utilities
        public void ConfigureModuleCatalog()
        {
            regionManager.Regions["GlobalRegion"].Add(unityContainer.Resolve<ContentView>());
        }
        #endregion
    }
}
