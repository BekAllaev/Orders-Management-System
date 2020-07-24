using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.WPFClient.Infrastructure.Events
{
    /// <summary>
    /// Fires when navigation starts
    /// </summary>
    public class OnNavigatedToEvent : PubSubEvent 
    { 
        public OnNavigatedToEvent(string titleOfCurrentModule)
        {
            TitleOfCurrentModule = titleOfCurrentModule;
        }

        public string TitleOfCurrentModule { set; get; }
    }
}
