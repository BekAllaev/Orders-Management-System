using OMS.Data;
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
    public class InvoiceInfoService : IInvoiceInfoService
    {
        INorthwindRepository northwindRepository;

        public InvoiceInfoService(INorthwindRepository northwindRepository)
        {
            this.northwindRepository = northwindRepository;
        }

        public async Task<IEnumerable<OrderDetailObject>> GetOrderDetailsInfo(int orderId)
        {
            var orderDetails = (await northwindRepository.GetOrderDetails()).Where(orderDetail => orderDetail.OrderID == orderId);

            var products = await northwindRepository.GetProducts();

            await Task.Run(() =>
            {
                orderDetails.ToList().ForEach(orderDetail =>
                {
                    orderDetail.Product = products.First(product => product.ProductID == orderDetail.ProductID);
                });
            });

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
            var order = (await northwindRepository.GetOrders()).First(a => a.OrderID == orderId);

            var customer = (await northwindRepository.GetCustomers()).First(a => a.CustomerID == order.CustomerID);

            var employee = (await northwindRepository.GetEmployees()).First(a => a.EmployeeID == order.EmployeeID);

            order.Customer = customer;
            order.Employee = employee;

            var orderObject = new ObservableCollection<OrderObject>();

            orderObject.Add(new OrderObject()
            {
                OrderId = order.OrderID,
                OrderDate = order.OrderDate.Value,
                EmployeeName = order.Employee.FirstName + " " + order.Employee.LastName,
                CustomerName = order.Customer.CompanyName
            });

            return orderObject;
        }
    }
}
