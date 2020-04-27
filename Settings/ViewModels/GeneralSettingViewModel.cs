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
        #endregion

        #region Constructor
        public GeneralSettingViewModel(IUserSettingsRepository userSettingsRepository)
        {
            this.userSettingsRepository = userSettingsRepository;

            ChangePrimaryPalletCollorCommand = ReactiveCommand.Create<string>(ChangePrimaryPalletCollorExecute);
            ChangeSecondaryPalletCollorCommand = ReactiveCommand.Create<string>(ChangeSecondaryPalletCollorExecute);
        }
        #endregion

        #region Commands
        public ReactiveCommand<string,Unit> ChangePrimaryPalletCollorCommand { get; }

        private void ChangePrimaryPalletCollorExecute(string color)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            Color newPrimaryColor = (Color)ColorConverter.ConvertFromString(color);

            //Change all of the primary colors
            theme.SetPrimaryColor(newPrimaryColor);

            //Change the app's current theme
            paletteHelper.SetTheme(theme);

            userSettingsRepository.WriteSetting("AppPrimaryColor", color);
        }

        public ReactiveCommand<string,Unit> ChangeSecondaryPalletCollorCommand { get; }

        private void ChangeSecondaryPalletCollorExecute(string color)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            Color newPrimaryColor = (Color)ColorConverter.ConvertFromString(color);

            //Change all of the primary colors
            theme.SetSecondaryColor(newPrimaryColor);

            //Change the app's current theme
            paletteHelper.SetTheme(theme);

            userSettingsRepository.WriteSetting("AppSecondaryColor", color);
        }
        #endregion

        #region Properties
        #endregion
    }
}
