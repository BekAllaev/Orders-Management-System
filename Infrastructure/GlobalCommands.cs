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
        /// Command which execute navigation into module from which button Manage was pressed
        /// </summary>
        public static CompositeCommand NavigateToCompositeCommand = new CompositeCommand();

        /// <summary>
        /// Open(close) settings docking window
        /// </summary>
        public static CompositeCommand OpenSettingsCompositeCommand = new CompositeCommand();
    }
}
