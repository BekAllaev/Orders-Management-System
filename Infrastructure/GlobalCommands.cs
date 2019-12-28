using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class GlobalCommands
    {
        /// <summary>
        /// Execute navigation into module from which button Manage was pressed
        /// </summary>
        public static CompositeCommand NavigateToCompositeCommand = new CompositeCommand();

        /// <summary>
        /// Change IsChecked state of setting toggle button on Banner 
        /// </summary>
        public static CompositeCommand ChangeIsCheckedCompositeCommand = new CompositeCommand();

        /// <summary>
        /// Start animation of opening(closing) settings panel
        /// </summary>
        public static CompositeCommand OpenSettingsCompositeCommand = new CompositeCommand();
    }
}
