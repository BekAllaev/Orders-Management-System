using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Infrastructure.SettingsRepository;

namespace Settings.ViewModels
{
    public class GeneralSettingViewModel : ReactiveObject
    {
        #region Declarations
        IUserSettingsRepository userSettingsRepository;
        bool _isDarkMode;
        PaletteHelper paletteHelper;
        ITheme theme;
        #endregion

        #region Constructor
        public GeneralSettingViewModel(IUserSettingsRepository userSettingsRepository)
        {
            this.userSettingsRepository = userSettingsRepository;
            paletteHelper = new PaletteHelper();
            theme = paletteHelper.GetTheme();

            ChangePrimaryPalletCollorCommand = ReactiveCommand.Create<string>(ChangePrimaryPalletCollorExecute);
            ChangeSecondaryPalletCollorCommand = ReactiveCommand.Create<string>(ChangeSecondaryPalletCollorExecute);

            IsDarkMode = (bool)userSettingsRepository.ReadSetting("IsDarkTheme");

            this.WhenAnyValue(x => x.IsDarkMode)
                .Subscribe(isDark =>
                {
                    if (isDark)
                        theme.SetBaseTheme(Theme.Dark);
                    else
                        theme.SetBaseTheme(Theme.Light);

                    paletteHelper.SetTheme(theme);

                    userSettingsRepository.WriteSetting("IsDarkTheme", isDark);
                });
        }
        #endregion

        #region Commands
        public ReactiveCommand<string, Unit> ChangePrimaryPalletCollorCommand { get; }

        private void ChangePrimaryPalletCollorExecute(string color)
        {
            Color newPrimaryColor = (Color)ColorConverter.ConvertFromString(color);

            //Change all of the primary colors
            theme.SetPrimaryColor(newPrimaryColor);

            //Change the app's current theme
            paletteHelper.SetTheme(theme);

            userSettingsRepository.WriteSetting("AppPrimaryColor", color);
        }

        public ReactiveCommand<string, Unit> ChangeSecondaryPalletCollorCommand { get; }

        private void ChangeSecondaryPalletCollorExecute(string color)
        {
            Color newSecondaryColor = (Color)ColorConverter.ConvertFromString(color);

            //Change all of the secondary colors
            theme.SetSecondaryColor(newSecondaryColor);

            //Change the app's current theme
            paletteHelper.SetTheme(theme);

            userSettingsRepository.WriteSetting("AppSecondaryColor", color);
        }
        #endregion

        #region Properties
        public bool IsDarkMode
        {
            set { this.RaiseAndSetIfChanged(ref _isDarkMode, value); }
            get { return _isDarkMode; }
        }
        #endregion
    }
}
