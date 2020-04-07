using Prism.Regions;
using Infrastructure;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using ReactiveUI;

namespace Dashboard.Main
{
    public class DashboardMainViewModel : ReactiveObject, IRegionMemberLifetime
    {
        #region Declarations
        #endregion

        #region Constructors
        public DashboardMainViewModel()
        {
        }
        #endregion

        #region Properties
        public bool KeepAlive => true;
        #endregion

        #region Commands
        #endregion
    }
}
