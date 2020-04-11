using Infrastructure.SettingsRepository;
using Orders.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Services;

namespace Orders.Main
{
    public class OrdersMainViewModel : ReactiveObject, INavigationAware
    {
        #region Fields
        IRegionManager regionManager;
        IUserSettingsRepository userSettingsRepository;
        TitleUpdater changeDashboardTitleService;

        OrdersViews ordersCurrentView;

        string _switchButtonContent;
        string _currentView;
        #endregion

        #region Constructors
        public OrdersMainViewModel(IRegionManager regionManager, IUserSettingsRepository userSettingsRepository,TitleUpdater changeDashboardTitleService)
        {
            this.regionManager = regionManager;
            this.userSettingsRepository = userSettingsRepository;
            this.changeDashboardTitleService = changeDashboardTitleService;

            string ordersMainView = (string)userSettingsRepository.ReadSetting("OrdersMainView");

            SwitchViewCommand = new DelegateCommand(SwitchViewCommandExecute);

            switch (ordersMainView)
            {
                case "Journal View":
                    ordersCurrentView = OrdersViews.Journal;
                    SwitchButtonContent = "To Creation";
                    NameOfCurrentView = "Journal";
                    break;
                case "Create View":
                    ordersCurrentView = OrdersViews.Create;
                    SwitchButtonContent = "To Journal";
                    NameOfCurrentView = "Create";
                    break;
            }
        }
        #endregion

        #region Properties
        public string SwitchButtonContent
        {
            set { this.RaiseAndSetIfChanged(ref _switchButtonContent, value); }
            get { return _switchButtonContent; }
        }

        public string NameOfCurrentView
        {
            set { this.RaiseAndSetIfChanged(ref _currentView, value); }
            get { return _currentView; }
        }
        #endregion

        #region Command
        public DelegateCommand SwitchViewCommand { get; }

        private void SwitchViewCommandExecute()
        {
            switch (ordersCurrentView)
            {
                case OrdersViews.Create:
                    regionManager.RequestNavigate("OrdersCreateJournalRegion", "JournalView");
                    ordersCurrentView = OrdersViews.Journal;
                    SwitchButtonContent = "To Create";
                    NameOfCurrentView = "Journal";
                    break;
                case OrdersViews.Journal:
                    regionManager.RequestNavigate("OrdersCreateJournalRegion", "CreateView");
                    ordersCurrentView = OrdersViews.Create;
                    SwitchButtonContent = "To Journal";
                    NameOfCurrentView = "Create";
                    break;
            }
        }
        #endregion

        #region Implementation of INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string ordersMainView = (string)userSettingsRepository.ReadSetting("OrdersMainView");
            regionManager.RequestNavigate("OrdersCreateJournalRegion", ordersMainView.Replace(" ", ""));
            changeDashboardTitleService.UpdateDashboardTitle(navigationContext.Uri.ToString());
        }
        #endregion

        /// <summary>
        /// View that can be loaded into main view of Orders module
        /// </summary>
        enum OrdersViews { Create, Journal }
    }

}
