using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;

namespace Infrastructure
{
    public static class GlobalCommands
    {
        /// <summary>
        /// Command which executes navigation into module from which button Manage was pressed
        /// </summary>
        public static CompositeCommand NavigateToCompositeCommand = new CompositeCommand();
    }
}
