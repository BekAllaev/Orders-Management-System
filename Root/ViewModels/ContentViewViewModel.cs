using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Prism.Commands;

namespace Root.ViewModels
{
    public class ContentViewModel : BindableBase
    {
        #region Declarations
        IRegionManager regionManager;
        #endregion

        #region Constructor
        public ContentViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            NavigateToCommand = new DelegateCommand<string>(NavigateToExecute);
            //NavigateToSettingsCommand = new DelegateCommand<string>(NavigateToSettingsExecute);

            GlobalCommands.NavigateToCompositeCommand.RegisterCommand(NavigateToCommand);
            //GlobalCommands.NavigateToSettingsCompositeCommand.RegisterCommand(NavigateToSettingsCommand);
        }
        #endregion

        #region Commands

        #region NavigateToSettingsCommand
        //public DelegateCommand<string> NavigateToSettingsCommand { set; get; }

        //private void NavigateToSettingsExecute(string settingsView)
        //{
        //    regionManager.RequestNavigate("ContentRegion", settingsView);
        //}
        #endregion

        #region NavigateToCommand
        public DelegateCommand<string> NavigateToCommand { set; get; }

        private void NavigateToExecute(string targetView)
        {
            regionManager.RequestNavigate("ContentRegion", targetView);
        }
        #endregion

        #endregion
    }
}
