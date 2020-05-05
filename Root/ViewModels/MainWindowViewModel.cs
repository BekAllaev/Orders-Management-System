﻿using System;
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
using Infrastructure.SettingsRepository;
using Prism.Commands;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;

namespace Root.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Declarations
        IRegionManager regionManager;
        IUnityContainer unityContainer;
        IUserSettingsRepository userSettingsRepository;
        #endregion

        #region Constructors
        public MainWindowViewModel(IUnityContainer unityContainer,IRegionManager regionManager,IUserSettingsRepository userSettingsRepository)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
            this.userSettingsRepository = userSettingsRepository;

            SetTheme();
        }
        #endregion

        #region Utilities
        public void ConfigureModuleCatalog()
        {
            regionManager.Regions["GlobalRegion"].Add(unityContainer.Resolve<ContentView>());
        }

        private void SetTheme()
        {
            string defaultPrimaryColor = (string)userSettingsRepository.ReadSetting("AppPrimaryColor");
            bool isDefaultDarkTheme = (bool)userSettingsRepository.ReadSetting("IsDarkTheme");

            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            Color primaryColor = (Color)ColorConverter.ConvertFromString(defaultPrimaryColor);

            theme.SetPrimaryColor(primaryColor);
            if (isDefaultDarkTheme) theme.SetBaseTheme(Theme.Dark);

            paletteHelper.SetTheme(theme);
        }
        #endregion
    }
}
