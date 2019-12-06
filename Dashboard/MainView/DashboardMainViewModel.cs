using Prism.Regions;
using Infrastructure;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;

namespace Dashboard.MainView
{
    public class DashboardMainViewModel : BindableBase, IRegionMemberLifetime
    {
        #region Declarations
        DockState _currentDockState;
        #endregion

        #region Constructors
        public DashboardMainViewModel()
        {
            CurrentDockState = DockState.Hidden;

            OpenSettingsCommand = new DelegateCommand(OpenSettingsCommandExecute);

            GlobalCommands.OpenSettingsCompositeCommand.RegisterCommand(OpenSettingsCommand);
        }
        #endregion

        #region Properties
        public bool KeepAlive => true;

        public DockState CurrentDockState
        {
            set { SetProperty(ref _currentDockState, value); }
            get { return _currentDockState; }
        }
        #endregion

        #region Commands

        #region OpenSettingsCommand
        public DelegateCommand OpenSettingsCommand { set; get; }

        private void OpenSettingsCommandExecute()
        {
            if (CurrentDockState == DockState.Hidden) CurrentDockState = DockState.Dock;
            else if (CurrentDockState == DockState.Dock) CurrentDockState = DockState.Hidden;
        }
        #endregion

        #endregion
    }
}
