using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Settings.Main;
using Infrastructure.SettingsRepository;
using Prism.Commands;
using Banner.Main;
using ReactiveUI;

namespace Root.ViewModels
{
    public class ContentViewModel : ReactiveObject
    {
        #region Declarations
        IRegionManager regionManager;
        #endregion

        #region Constructor
        public ContentViewModel(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this.regionManager = regionManager;

            NavigateToCommand = new DelegateCommand<string>(NavigateToExecute);

            GlobalCommands.NavigateToCompositeCommand.RegisterCommand(NavigateToCommand);
        }
        #endregion

        #region Commands

        #region NavigateToCommand
        public DelegateCommand<string> NavigateToCommand { set; get; }

        private void NavigateToExecute(string targetView)
        {
            SettingsMainView settingsPanelView = (SettingsMainView)regionManager.Regions["SettingRegion"].ActiveViews.First();

            if (settingsPanelView.IsOpen)
            {
                GlobalCommands.OpenSettingsCompositeCommand.Execute(null);
                GlobalCommands.ChangeIsCheckedCompositeCommand.Execute(null);
            }

            regionManager.RequestNavigate("ContentRegion", targetView);
        }
        #endregion

        #endregion
    }
}
