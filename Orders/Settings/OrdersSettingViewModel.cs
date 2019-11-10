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
            _availableViews = new List<string>()
            {
                "Create View",
                "Journal View"
            };
        }

        public string SelectedView
        {
            set { _selectedView = value; }
            get
            {
                Properties.Settings.Default.OrdersMainView = _selectedView.Replace(" ", "");

                Properties.Settings.Default.Save();

                return _selectedView;
            }
        }

        public List<string> AvailableViews
        {
            get { return _availableViews; }
        }
    }
}
