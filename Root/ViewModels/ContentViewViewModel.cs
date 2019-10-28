using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Prism.Commands;

namespace Root.ViewModels
{
    public class ContentViewModel : BindableBase
    {
        IRegionManager regionManager;

        public ContentViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            NavigateToCommand = new DelegateCommand<string>(NavigateToExecute);

            GlobalCommands.NavigateToCompositeCommand.RegisterCommand(NavigateToCommand);
        }

        public DelegateCommand<string> NavigateToCommand { set; get; }

        private void NavigateToExecute(string targetView)
        {
            regionManager.RequestNavigate("ContentRegion", targetView);
        }
    }
}
