using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using Settings.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Mvvm;

namespace Banner.Main
{
    public class BannerMainViewModel : BindableBase
    {
        #region Declarations
        IRegionManager regionManager;
        bool _isChecked;
        #endregion

        #region Constructors
        public BannerMainViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            OpenSettingPanelCommand = new DelegateCommand(OpenSettingPanelCommandExecute);

            ChangeIsCheckedCommand = new DelegateCommand(ChangeIsCheckedExecute);

            GlobalCommands.ChangeIsCheckedCompositeCommand.RegisterCommand(ChangeIsCheckedCommand);
        }
        #endregion

        #region Properties
        public DelegateCommand OpenSettingPanelCommand { get; }

        public DelegateCommand ChangeIsCheckedCommand { get; }

        public bool IsChecked 
        { 
            set { SetProperty(ref _isChecked, value); } 
            get { return _isChecked; } 
        }
        #endregion

        #region Utilities 
        private void ChangeIsCheckedExecute()
        {
            IsChecked = IsChecked == true ? false : true;
        }

        private void OpenSettingPanelCommandExecute()
        {
            GlobalCommands.OpenSettingsCompositeCommand.Execute(null);
        }
        #endregion
    }
}
