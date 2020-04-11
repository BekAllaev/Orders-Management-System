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
using ReactiveUI;
using Infrastructure.Services;

namespace Dashboard.Main
{
    public class DashboardMainViewModel : ReactiveObject, IRegionMemberLifetime, INavigationAware
    {
        #region Declarations
        ChangeDashboardTitle changeDashboardTitleService;
        #endregion

        #region Constructors
        public DashboardMainViewModel(ChangeDashboardTitle changeDashboardTitleService)
        {
            this.changeDashboardTitleService = changeDashboardTitleService;
        }
        #endregion

        #region Properties
        public bool KeepAlive => true;

        public bool IsNavigationTarget(NavigationContext navigationContext) { return true; }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string nameOfCurrentModule = navigationContext.Uri.ToString();
            changeDashboardTitleService.UpdateDashboardTitle(nameOfCurrentModule);
        }
        #endregion

        #region Commands
        #endregion
    }
}
