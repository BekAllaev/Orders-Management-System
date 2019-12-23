using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Settings.MainView;
using Infrastructure.SettingsRepository;
using Prism.Commands;

namespace Root.ViewModels
{
    public class ContentViewModel : BindableBase
    {
        #region Declarations
        IRegionManager regionManager;
        #endregion

        #region Constructor
        public ContentViewModel(IRegionManager regionManager,IUnityContainer unityContainer)
        {
            this.regionManager = regionManager;

            NavigateToCommand = new DelegateCommand<string>(NavigateToExecute);

            unityContainer.RegisterInstance<IUserSettingsRepository>(new UserSettingsRepository());

            GlobalCommands.NavigateToCompositeCommand.RegisterCommand(NavigateToCommand);
        }
        #endregion

        #region Commands

        #region NavigateToCommand
        public DelegateCommand<string> NavigateToCommand { set; get; }

        private void NavigateToExecute(string targetView)
        {
            SettingsPanelView settingsPanelView = (SettingsPanelView)regionManager.Regions["SettingRegion"].ActiveViews.First();

            if (settingsPanelView.IsOpen) settingsPanelView.StartAnimation();

            regionManager.RequestNavigate("ContentRegion", targetView);
        }
        #endregion

        #endregion
    }
}
