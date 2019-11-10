using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.OrdersSettings
{
    public class MainViewSettingViewModel : BindableBase, INavigationAware
    {
        List<string> _availableViews;

        public MainViewSettingViewModel()
        {
            _availableViews = new List<string>()
            {
                "Create View",
                "Journal View"
            };
        }

        public string SelectedView { set; get; }

        public List<string> AvailableViews
        {
            get { return _availableViews; }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Properties.Settings.Default.OrdersMainView = SelectedView.Replace(" ", "");

            Properties.Settings.Default.Save();
        }
    }
}
