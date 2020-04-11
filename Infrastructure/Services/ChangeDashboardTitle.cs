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
    public class ChangeDashboardTitle
    {
        public void UpdateDashboardTitle(string targetView)
        {
            string parameterView;//view that will be passed as parameter

            parameterView = targetView.Substring(0, targetView.Length - 8);

            OnNavigatedToEvent onNavigatedToEvent = new OnNavigatedToEvent(parameterView);

            MessageBus.Current.SendMessage<OnNavigatedToEvent>(onNavigatedToEvent);
        }
    }
}
