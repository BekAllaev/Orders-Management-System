using OMS.WPFClient.Modules.Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.WPFClient.Infrastructure.Services.InvoiceInfoService
{
    public class InvoiceInfoWeb : IInvoiceInfoService
    {
        public Task<IEnumerable<OrderDetailObject>> GetOrderDetailsInfo(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderObject>> GetOrdersInfo(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
