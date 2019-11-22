using Orders.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Settings;

namespace Orders.Main
{
    public class OrdersMainViewModel : BindableBase
    {
        #region Fields
        IRegionManager regionManager;
        IUserSettingsRepository userSettingsRepository;
        #endregion

        #region Constructors
        public OrdersMainViewModel(IRegionManager regionManager, IUserSettingsRepository userSettingsRepository)
        {
            this.regionManager = regionManager;
            this.userSettingsRepository = userSettingsRepository;
        }
        #endregion

        #region Properties

        #endregion
    }
}
