using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Settings
{
    public class OrdersSettingViewModel
    {
        List<string> _availableViews;
        string _selectedView;

        public OrdersSettingViewModel()
        {
            SelectedView = "Create View";

            _availableViews = new List<string>()
            {
                "Create View",
                "Journal View"
            };
        }

        public string SelectedView
        {
            set
            {
                _selectedView = value;

                Properties.Settings.Default.OrdersMainView = _selectedView.Replace(" ", "");

                Properties.Settings.Default.Save();
            }
            get { return _selectedView; }
        }

        public List<string> AvailableViews
        {
            get { return _availableViews; }
        }
    }
}
