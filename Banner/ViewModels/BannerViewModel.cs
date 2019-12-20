using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using Settings.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Banner.ViewModels
{
    public class BannerViewModel
    {
        IRegionManager regionManager;

        public BannerViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            OpenSettingPanelCommand = new DelegateCommand(OpenSettingPanelCommandExecute);
        }

        public DelegateCommand OpenSettingPanelCommand { get; }

        private void OpenSettingPanelCommandExecute()
        {
            SettingsPanelView settingsPanelView = (SettingsPanelView)regionManager.Regions["SettingRegion"].ActiveViews.First();

            settingsPanelView.StartAnimation();
        }
    }
}
