using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SettingsRepository
{
    public class UserSettingsRepository : IUserSettingsRepository
    {
        private Properties.Settings Default = Properties.Settings.Default; //Instance of the user settings. ???I think there is better comment for this field???

        public object ReadSetting(string settingName)
        {
            return Default[settingName];
        }

        public void WriteSetting(string settingName, object value)
        {
            Default[settingName] = value;

            Default.Save();
        }
    }
}
