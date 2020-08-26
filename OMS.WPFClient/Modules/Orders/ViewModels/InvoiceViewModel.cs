using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Data;
using OMS.DataAccessLocal;
using OMS.WPFClient.Infrastructure.Services.InvoiceInfoService;
using OMS.WPFClient.Modules.Orders.Events;
using Prism.Regions;
using ReactiveUI;

namespace OMS.WPFClient.Modules.Orders.ViewModels
{
    public class InvoiceViewModel : ReactiveObject, INavigationAware
    {
        INorthwindRepository northwindRepository;
        IInvoiceInfoService invoiceInfoService;

        public InvoiceViewModel(INorthwindRepository northwindRepository, IInvoiceInfoService invoiceInfoService)
        {
            this.northwindRepository = northwindRepository;
            this.invoiceInfoService = invoiceInfoService;

            MessageBus.Current.Listen<NewOrderCreated>().
                Subscribe(orderCreatedEvent => OnNewOrderCreated(orderCreatedEvent.OrderId));
        }

        public ObservableCollection<OrderObject> Orders { get; set; }
        public ObservableCollection<OrderDetailObject> OrderDetails { get; set; }

        #region INavigationAware interface implementation
        public bool IsNavigationTarget(NavigationContext navigationContext) { return true; }

        public void OnNavigatedFrom(NavigationContext navigationContext) { }

        public void OnNavigatedTo(NavigationContext navigationContext) { }
        #endregion

        private async void OnNewOrderCreated(int id)
        {
            Orders = new ObservableCollection<OrderObject>(await invoiceInfoService.GetOrdersInfo(id));

            OrderDetails = new ObservableCollection<OrderDetailObject>(await invoiceInfoService.GetOrderDetailsInfo(id));

            MessageBus.Current.SendMessage<OrerDataCreated>(new OrerDataCreated());
        }
    }
    #region Screen objects
        public class OrderObject
        {
            public int OrderId { get; set; }
            public string CustomerName { get; set; }
            public string EmployeeName { get; set; }
            public DateTime OrderDate { get; set; }
        }

        public class OrderDetailObject
        {
            public int OrderId { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public short Quantity { get; set; }
            public float Discount { get; set; }
            public decimal SubTotal { get; set; }
        }
        #endregion
}
