using Prism.Regions;
using OMS.WPFClient.Infrastructure;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using ReactiveUI;
using OMS.WPFClient.Infrastructure.Services;

namespace OMS.WPFClient.Modules.Dashboard.Main
{
    public class DashboardMainViewModel : ReactiveObject, IRegionMemberLifetime, INavigationAware
    {
        #region Declarations
        TitleUpdater changeDashboardTitleService;
        #endregion

        #region Constructors
        public DashboardMainViewModel(TitleUpdater changeDashboardTitleService)
        {
            this.changeDashboardTitleService = changeDashboardTitleService;
        }
        #endregion

        #region Implementation of IRegionMemberLifetime
        public bool KeepAlive => true;
        #endregion

        #region Implementation of INavigationAware
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
