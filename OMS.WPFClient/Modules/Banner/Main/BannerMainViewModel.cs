using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using OMS.WPFClient.Modules.Settings.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using OMS.WPFClient.Infrastructure;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Mvvm;
using ReactiveUI;
using OMS.WPFClient.Infrastructure.Events;

namespace OMS.WPFClient.Modules.Banner.Main
{
    public class BannerMainViewModel : ReactiveObject
    {
        #region Declarations
        IRegionManager regionManager;

        bool _isChecked;
        string _currentModuleTitle;
        #endregion

        #region Constructors
        public BannerMainViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            OpenSettingPanelCommand = new DelegateCommand(OpenSettingPanelCommandExecute);

            ChangeIsCheckedCommand = new DelegateCommand(ChangeIsCheckedExecute);

            GlobalCommands.ChangeIsCheckedCompositeCommand.RegisterCommand(ChangeIsCheckedCommand);

            MessageBus.Current.Listen<OnNavigatedToEvent>().
                Subscribe(titleOfModule => CurrentModuleTitle = titleOfModule.TitleOfCurrentModule);
        }
        #endregion

        #region Properties
        public DelegateCommand OpenSettingPanelCommand { get; }

        public DelegateCommand ChangeIsCheckedCommand { get; }

        public bool IsChecked
        {
            set { this.RaiseAndSetIfChanged(ref _isChecked, value); }
            get { return _isChecked; }
        }

        public string CurrentModuleTitle
        {
            set { this.RaiseAndSetIfChanged(ref _currentModuleTitle, value); }
            get { return _currentModuleTitle; }
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
