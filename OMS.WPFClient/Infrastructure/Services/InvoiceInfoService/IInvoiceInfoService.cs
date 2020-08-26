using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.WPFClient.Modules.Orders.ViewModels;

namespace OMS.WPFClient.Infrastructure.Services.InvoiceInfoService
{
    public interface IInvoiceInfoService
    {
        Task<IEnumerable<OrderObject>> GetOrdersInfo(int orderId);

        Task<IEnumerable<OrderDetailObject>> GetOrderDetailsInfo(int orderId);
    }
}
