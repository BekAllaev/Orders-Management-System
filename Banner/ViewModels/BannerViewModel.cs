using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using Settings.MainView;
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Mvvm;

namespace Banner.ViewModels
{
    public class BannerViewModel : BindableBase
    {
        IRegionManager regionManager;
        bool _isChecked;

        public BannerViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            OpenSettingPanelCommand = new DelegateCommand(OpenSettingPanelCommandExecute);

            ChangeIsCheckedCommand = new DelegateCommand(ChangeIsCheckedExecute);

            GlobalCommands.ChangeIsCheckedCompositeCommand.RegisterCommand(ChangeIsCheckedCommand);
        }

        private void ChangeIsCheckedExecute()
        {
            IsChecked = IsChecked == true ? false : true;
        }

        public DelegateCommand OpenSettingPanelCommand { get; }

        public DelegateCommand ChangeIsCheckedCommand { get; }

        public bool IsChecked 
        { 
            set { SetProperty(ref _isChecked, value); } 
            get { return _isChecked; } 
        }

        private void OpenSettingPanelCommandExecute()
        {
            SettingsPanelView settingsPanelView = (SettingsPanelView)regionManager.Regions["SettingRegion"].ActiveViews.First();

            settingsPanelView.StartAnimation();
        }
    }
}
