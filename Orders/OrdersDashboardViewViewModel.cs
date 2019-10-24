using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders
{
    public class OrdersDashboardViewViewModel : BindableBase
    {
        #region Fields
        IRegionManager regionManager;
        #endregion

        #region Constructors
        public OrdersDashboardViewViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            //NavigateToOrdersModule = new DelegateCommand(ExecuteNavigateToOrdersModule);
        }

        #endregion

        #region Commands

        //public DelegateCommand NavigateToOrdersModule { get; }

        //private void ExecuteNavigateToOrdersModule()
        //{

        //}

        #endregion
    }
}
