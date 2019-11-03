using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.ViewModels
{
    public class CreateViewModel : BindableBase, IRegionMemberLifetime
    {
        #region Declarations
        IRegionManager regionManager;
        #endregion

        #region Construct
        public CreateViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            NavigateToJournalCommand = new DelegateCommand(ExecuteNavigateToJournal);
        }
        #endregion

        #region
        public bool KeepAlive => true;
        #endregion

        #region Commands
        public DelegateCommand NavigateToJournalCommand { get; }

        private void ExecuteNavigateToJournal()
        {
            regionManager.RequestNavigate("OrdersManagmentRegion", "JournalView");
        }
        #endregion
    }
}
