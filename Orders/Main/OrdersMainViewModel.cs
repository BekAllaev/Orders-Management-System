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
        #endregion

        #region Constructors
        public OrdersMainViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            regionManager.Regions["OrdersManagmentRegion"].Add(typeof(CreateView));
            //regionManager.RequestNavigate("OrdersManagmentRegion", "CreateView");
        }

        #endregion

        #region Commands



        #endregion
    }
}
