using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Settings
{
    public class OrdersSettingViewModel
    {
        #region Declaration
        List<string> _availableViews;
        string _selectedView;
        IRegionManager regionManager;
        #endregion

        #region Constructor
        public OrdersSettingViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            SelectedView = Properties.Settings.Default.OrdersMainView;

            _availableViews = new List<string>()
            {
                "Create View",
                "Journal View"
            };
        }
        #endregion

        #region Properties
        public string SelectedView
        {
            set
            {
                _selectedView = value;

                Properties.Settings.Default.OrdersMainView = _selectedView;

                Properties.Settings.Default.Save();

                regionManager.RequestNavigate("OrdersManagmentRegion", _selectedView.Replace(" ", ""));
            }
            get { return _selectedView; }
        }

        public List<string> AvailableViews
        {
            get { return _availableViews; }
        }
        #endregion
    }
}
