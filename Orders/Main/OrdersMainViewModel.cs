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
    public class OrdersMainViewModel : BindableBase, INavigationAware
    {
        #region Fields
        IRegionManager regionManager;
        #endregion

        #region Constructors
        public OrdersMainViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        #endregion

        #region Properties

        #endregion

        #region Implementation of INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            regionManager.RequestNavigate("OrdersManagmentRegion", Properties.Settings.Default.OrdersMainView.Replace(" ", ""));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Type targetType = Type.GetType("Orders.Views." + Properties.Settings.Default.OrdersMainView.Replace(" ", ""));

            regionManager.RegisterViewWithRegion("OrdersManagmentRegion", targetType);
        }
        #endregion
    }
}
