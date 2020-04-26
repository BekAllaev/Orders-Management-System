using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Events;
using ReactiveUI;

namespace Infrastructure.Services
{
    /// <summary>
    /// Changes title of current module on dashboard
    /// </summary>
    public class TitleUpdater
    {
        public void UpdateDashboardTitle(string targetView)
        {
            string parameterView;//view that will be passed as parameter

            parameterView = targetView.Substring(0, targetView.Length - 8);//Since every module`s main view name have this patter "name of module + "MainView" " we delete part "MainView" and get name of module 

            OnNavigatedToEvent onNavigatedToEvent = new OnNavigatedToEvent(parameterView);

            MessageBus.Current.SendMessage<OnNavigatedToEvent>(onNavigatedToEvent);
        }
    }
}
