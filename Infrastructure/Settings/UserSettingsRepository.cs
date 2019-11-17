using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Settings
{
    public class UserSettingsRepository : IUserSettingsRepository
    {
        private Properties.Settings Default = Properties.Settings.Default; //Instance of the user settings. ???I think there is better comment for this field???

        public object ReadSetting(string settingName)
        {
            return Default.Properties[settingName];
        }

        public void WriteSetting(string settingName, object value)
        {
            Default.OrdersMainView = (string)value;

            Default.Save();
        }
    }
}
