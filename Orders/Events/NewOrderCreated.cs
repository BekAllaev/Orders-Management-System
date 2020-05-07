using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace Orders.Events
{
    /// <summary>
    /// Event fires when new order was created or order in journal was pressed
    /// </summary>
    public class NewOrderCreated : PubSubEvent
    {  
        public int OrderId { set; get; }
    }
}
