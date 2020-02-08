using Infrastructure.SettingsRepository;
using Orders.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Main
{
    public class OrdersMainViewModel : BindableBase
    {
        #region Fields
        IRegionManager regionManager;
        IUserSettingsRepository userSettingsRepository;

        OrdersViews ordersCurrentView;

        string _switchButtonContent;
        string _currentView;
        #endregion

        #region Constructors
        public OrdersMainViewModel(IRegionManager regionManager, IUserSettingsRepository userSettingsRepository)
        {
            this.regionManager = regionManager;
            this.userSettingsRepository = userSettingsRepository;

            SwitchViewCommand = new DelegateCommand(SwitchViewCommandExecute);

            string ordersMainView = (string)userSettingsRepository.ReadSetting("OrdersMainView");

            switch (ordersMainView)
            {
                case "Journal View":
                    ordersCurrentView = OrdersViews.Journal;
                    SwitchButtonContent = "Navigate to Creation";
                    NameOfCurrentView = "Journal";
                    break;
                case "Create View":
                    ordersCurrentView = OrdersViews.Create;
                    SwitchButtonContent = "Navigate to Journal";
                    NameOfCurrentView = "Create";
                    break;
            }
        }
        #endregion

        #region Properties
        public string SwitchButtonContent 
        {
            set { SetProperty(ref _switchButtonContent, value); }
            get { return _switchButtonContent; }
        }

        public string NameOfCurrentView
        {
            set { SetProperty(ref _currentView, value); }
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
                    regionManager.RequestNavigate("OrdersManagmentRegion", "JournalView");
                    ordersCurrentView = OrdersViews.Journal;
                    SwitchButtonContent = "Navigate to Create";
                    NameOfCurrentView = "Journal";
                    break;
                case OrdersViews.Journal:
                    regionManager.RequestNavigate("OrdersManagmentRegion", "CreateView");
                    ordersCurrentView = OrdersViews.Create;
                    SwitchButtonContent = "Navigate to Journal";
                    NameOfCurrentView = "Create";
                    break;
            }
        }
        #endregion

        /// <summary>
        /// View that can be loaded into main view of Orders module
        /// </summary>
        enum OrdersViews { Create, Journal }
    }

}
