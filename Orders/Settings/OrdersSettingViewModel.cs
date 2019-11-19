using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using Infrastructure.Settings;
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
        IUserSettingsRepository userSettingsRepository;
        #endregion

        #region Constructor
        public OrdersSettingViewModel(IRegionManager regionManager, IUserSettingsRepository userSettingsRepository)
        {
            this.regionManager = regionManager;
            this.userSettingsRepository = userSettingsRepository;

            SelectedView = (string)userSettingsRepository.ReadSetting("OrdersMainView");

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

                userSettingsRepository.WriteSetting("OrdersMainView", _selectedView);

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
