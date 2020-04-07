using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace Infrastructure.Events
{
    /// <summary>
    /// Event that raised when navigation occurs
    /// </summary>
    public class OnNavigatedToEvent : PubSubEvent<string>
    {
    }
}
