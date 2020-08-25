using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMSWebMini.Data;
using OMSWebMini.Model;

namespace OMSWebMini.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        NorthwindContext northwindContext;

        public StatisticsController(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsByCategory>>> GetProductsByCategories()
        {
            var productsByCategories = await northwindContext.Categories.Select(category => new ProductsByCategory
            {
                CategoryName = category.CategoryName,
                NumberOfProducts = category.Products.Count
            }
            ).ToListAsync();

            return productsByCategories;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesByEmployee>>> GetSalesByEmployees()
        {
            var employees = await northwindContext.Employees.Include(employee=>employee.Orders).
                ThenInclude(employeeOrder=>employeeOrder.OrderDetails).
                ToListAsync();

            return await Task.Run(() =>
            {
                var salesByEmployees = employees.Select(employee => new SalesByEmployee
                {
                    LastName = employee.LastName,
                    Sales = employee.Orders.Sum(order => order.OrderDetails.Sum(orderDetail => orderDetail.Quantity * orderDetail.UnitPrice))
                }).OrderBy(salesByEmployee => salesByEmployee.Sales).ToList();

                return salesByEmployees;
            });
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomersByCountry>>> GetCustomersByCountries()
        {
            var groupedCustomers = northwindContext.Customers.GroupBy(customer => customer.Country);

            var customersByCountries = await groupedCustomers.Select(customerGroup => new CustomersByCountry 
            { 
                CountryName = customerGroup.Key, 
                CustomersCount = customerGroup.Count() 
            }).ToListAsync();

            return customersByCountries;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchasesByCustomer>>> GetPurchasesByCustomers()
        {
            var customers = await northwindContext.Customers.Include(customer => customer.Orders).
                ThenInclude(customerOrder => customerOrder.OrderDetails).
                ToListAsync();

            return await Task.Run(() =>
            {
                var purchasesByCustomers = customers.Select(customer => new PurchasesByCustomer
                {
                    CompanyName = customer.CompanyName,
                    Purchases = customer.Orders.Sum(order => order.OrderDetails.Sum(orderDetail => orderDetail.Quantity * orderDetail.UnitPrice))
                }).OrderByDescending(purchasesByCustomer => purchasesByCustomer.Purchases).Take(10).ToList();

                return purchasesByCustomers;
            });
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersByCountry>>> GetOrdersByCountries()
        {
            var groupedOrders = northwindContext.Orders.GroupBy(order => order.Customer.Country);

            var ordersByCountries = await groupedOrders.Select(orderGroup => new OrdersByCountry { Country = orderGroup.Key, NumberOfOrders = orderGroup.Count() }).
                OrderByDescending(ordersByCountry => ordersByCountry.NumberOfOrders).
                Take(10).
                ToListAsync();

            return ordersByCountries;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesByCategory>>> GetSalesByCategories()
        {
            var groupedOrderDetail = northwindContext.OrderDetails.GroupBy(orderDetail => orderDetail.Product.Category.CategoryName);

            var salesByCategories = await groupedOrderDetail.Select(orderDetailGroup => new SalesByCategory
            {
                Category = orderDetailGroup.Key,
                Sales = orderDetailGroup.Sum(orderDetail => orderDetail.Quantity * orderDetail.UnitPrice)
            }).OrderByDescending(salesByCategory => salesByCategory.Sales).ToListAsync();

            return salesByCategories;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesByCountry>>> GetSalesByCountries()
        {
            var groupedOrderDetails = northwindContext.OrderDetails.GroupBy(orderDetail => orderDetail.Order.Customer.Country);

            var salesByCountries = await groupedOrderDetails.Select(orderDetailGroup => new SalesByCountry
            {
                Country = orderDetailGroup.Key,
                Sales = orderDetailGroup.Sum(orderDetail => orderDetail.Quantity * orderDetail.UnitPrice)
            }).OrderBy(salesByCountry=>salesByCountry.Sales).ToListAsync();

            return salesByCountries;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<string>> GetSummary(string summaryType)
        {
            string format = "$ ###,###.###";

            switch (summaryType)
            {
                case "OverallSalesSum":
                    return (await northwindContext.OrderDetails.SumAsync(a => a.Quantity * a.UnitPrice)).ToString(format);
                case "OrdersQuantity":
                    return (await northwindContext.Orders.CountAsync()).ToString();
                case "AverageCheck":
                case "MaxCheck":
                case "MinCheck":
                    var groupedOrderDetails = northwindContext.OrderDetails.GroupBy(od => od.OrderId);
                    var ordersChecks = await groupedOrderDetails.Select(god => new { Sales = god.Sum(a => a.Quantity * a.UnitPrice) }).ToListAsync();

                    if (summaryType == "MaxCheck") return ordersChecks.Max(a => a.Sales).ToString(format);
                    else if (summaryType == "AverageCheck") return ordersChecks.Average(a => a.Sales).ToString(format);
                    else return ordersChecks.Min(a => a.Sales).ToString(format);
                default:
                    return BadRequest();
            }
        }
    }

    #region Screen objects
    public class ProductsByCategory
    {
        public string CategoryName { set; get; }
        public int NumberOfProducts { set; get; }
    }

    public class SalesByEmployee
    {
        public string LastName { set; get; }

        public decimal Sales { set; get; }
    }

    public class CustomersByCountry
    {
        public string CountryName { set; get; }

        public int CustomersCount { set; get; }
    }

    public class PurchasesByCustomer
    {
        public string CompanyName { set; get; }
        public decimal Purchases { set; get; }
    }

    public class OrdersByCountry
    {
        public string Country { set; get; }
        public int NumberOfOrders { set; get; }
    }

    public class SalesByCategory
    {
        public string Category { set; get; }
        public decimal Sales { set; get; }
    }

    public class SalesByCountry
    {
        public string Country { set; get; }
        public decimal Sales { set; get; }
    }
    #endregion 
}
