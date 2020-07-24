using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.WPFClient.Infrastructure.SettingsRepository
{
    /// <summary>
    /// Represent methods which works with user scope settings
    /// </summary>
    public interface IUserSettingsRepository
    {
        /// <summary>
        /// Write value to the settings
        /// </summary>
        /// <param name="settingName">
        /// Setting`s name
        /// </param>
        /// <param name="value">
        /// Value to write to the setting
        /// </param>
        void WriteSetting(string settingName,object value);

        /// <summary>
        /// Get value of the setting
        /// </summary>
        /// <param name="settingName">
        /// Name of the setting from which to read value
        /// </param>
        /// <returns>
        /// Value of the setting 
        /// </returns>
        object ReadSetting(string settingName);
    }
}
