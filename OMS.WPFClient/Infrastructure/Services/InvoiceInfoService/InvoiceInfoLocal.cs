using OMS.DataAccessLocal;
using OMS.WPFClient.Modules.Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.WPFClient.Infrastructure.Services.InvoiceInfoService
{
    public class InvoiceInfoLocal : IInvoiceInfoService
    {
        NorthwindContext northwindContext;

        public InvoiceInfoLocal(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
        }

        public async Task<IEnumerable<OrderDetailObject>> GetOrderDetailsInfo(int orderId)
        {
            var orderDetails = await northwindContext.Order_Details.Where(orderDetail => orderDetail.OrderID == orderId).ToListAsync();

            var orderDetailObjects = new ObservableCollection<OrderDetailObject>
                (orderDetails.Select(o => new OrderDetailObject()
                {
                    OrderId = o.OrderID,
                    ProductName = o.Product.ProductName,
                    UnitPrice = o.UnitPrice,
                    Quantity = o.Quantity,
                    Discount = o.Discount * 100,
                    SubTotal = (decimal)((float)o.UnitPrice * o.Quantity * (1 - o.Discount))
                }));

            return orderDetailObjects;
        }

        public async Task<IEnumerable<OrderObject>> GetOrdersInfo(int orderId)
        {
            var orders = await northwindContext.Orders.Where(order => order.OrderID == orderId).ToListAsync();

            var orderObject = new ObservableCollection<OrderObject>
                (orders.Select(o => new OrderObject()
                {
                    OrderId = o.OrderID,
                    OrderDate = o.OrderDate.Value,
                    EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName,
                    CustomerName = o.Customer.CompanyName
                }));

            return orderObject;
        }
    }
}
