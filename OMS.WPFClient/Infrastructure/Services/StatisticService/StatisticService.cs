using DynamicData;
using DynamicData.Binding;
using OMS.Data;
using OMS.Data.Models;
using OMS.DataAccessLocal;
using OMS.WPFClient.Modules.Dashboard.ViewModels;
using ReactiveUI;
using Syncfusion.Data.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OMS.WPFClient.Infrastructure.Services.StatisticService
{
    public class StatisticService : ReactiveObject, IStatisticService
    {
        #region Declarations
        INorthwindRepository northwindRepository;

        SourceList<Customer> customersList;
        SourceList<Order_Detail> orderDetailsList;
        SourceList<Product> productsList;
        SourceList<Order> ordersList;

        ReadOnlyObservableCollection<CustomersByCountry> _customersByCountries;
        ReadOnlyObservableCollection<PurchasesByCustomers> _purchasesByCustomers;
        ReadOnlyObservableCollection<EmployeeSales> _salesByEmployees;
        ReadOnlyObservableCollection<ProductsByCateogries> _productsByCategories;
        ReadOnlyObservableCollection<SalesByCountry> _salesByCountries;
        ReadOnlyObservableCollection<OrdersByCountry> _ordersByCountries;
        ReadOnlyObservableCollection<SalesByCategory> _salesByCategories;

        IObservable<IChangeSet<CustomersByCountry>> customersByCountries;
        IObservable<IChangeSet<PurchasesByCustomers>> purchasesByCustomers;
        IObservable<IChangeSet<EmployeeSales>> salesByEmployees;
        IObservable<IChangeSet<ProductsByCateogries>> productsByCategories;
        IObservable<IChangeSet<OrdersByCountry>> ordersByCountries;
        IObservable<IChangeSet<SalesByCountry>> salesByCountries;
        IObservable<IChangeSet<SalesByCategory>> salesByCategories;
        #endregion

        public StatisticService(INorthwindRepository northwindRepository)
        {
            this.northwindRepository = northwindRepository;

            productsList = new SourceList<Product>();
            customersList = new SourceList<Customer>();
            orderDetailsList = new SourceList<Order_Detail>();
            ordersList = new SourceList<Order>();

            #region Customers statistics
            customersByCountries = customersList.Connect().
                GroupOn(customer => customer.Country).
                Transform(customersGroup => new CustomersByCountry() { CountryName = customersGroup.GroupKey, CustomersCount = customersGroup.List.Count }).
                ObserveOnDispatcher().
                Bind(out _customersByCountries);

            purchasesByCustomers = orderDetailsList.Connect().
                Transform(orderDetail => new { CompanyName = orderDetail.Order.Customer.CompanyName, PurchaseByOrderDetail = orderDetail.UnitPrice * orderDetail.Quantity }).
                GroupOn(orderDetail => orderDetail.CompanyName).
                Transform(groupOfOrderDetails => new PurchasesByCustomers() { CompanyName = groupOfOrderDetails.GroupKey, Purchases = groupOfOrderDetails.List.Items.Sum(a => a.PurchaseByOrderDetail) }).
                Sort(SortExpressionComparer<PurchasesByCustomers>.Descending(a => a.Purchases)).
                Top(10).
                ObserveOnDispatcher().
                Bind(out _purchasesByCustomers);
            #endregion

            #region Employees statistics
            salesByEmployees = orderDetailsList.Connect().
                Transform(orderDetail => new { LastName = orderDetail.Order.Employee.LastName, SaleByOrderDetail = orderDetail.UnitPrice * orderDetail.Quantity }).
                GroupOn(orderDetail => orderDetail.LastName).
                Transform(groupOfOrderDetail => new EmployeeSales() { LastName = groupOfOrderDetail.GroupKey, Sales = groupOfOrderDetail.List.Items.Sum(a => a.SaleByOrderDetail) }).
                ObserveOnDispatcher().
                Sort(SortExpressionComparer<EmployeeSales>.Ascending(a => a.Sales)).
                Bind(out _salesByEmployees);
            #endregion

            #region Products statistics
            productsByCategories = productsList.Connect().
                GroupOn(product => product.Category.CategoryName).
                Transform(groupOfProducts => new ProductsByCateogries() { CategoryName = groupOfProducts.GroupKey, NumberOfProducts = groupOfProducts.List.Count }).
                ObserveOnDispatcher().
                Bind(out _productsByCategories);
            #endregion

            #region Orders statistics
            ordersByCountries = ordersList.Connect().
                GroupOn(order => order.Customer.Country).
                Transform(groupOfOrders => new OrdersByCountry() { Country = groupOfOrders.GroupKey, NumberOfOrders = groupOfOrders.List.Count }).
                ObserveOnDispatcher().
                Top(10).
                Bind(out _ordersByCountries);

            salesByCountries = orderDetailsList.Connect().Transform(orderDetail => new { Country = orderDetail.Order.Customer.Country, SaleByOrderDetail = orderDetail.UnitPrice * orderDetail.Quantity }).
                GroupOn(orderDetail => orderDetail.Country).
                Transform(groupOfOrderDetails => new SalesByCountry() { Country = groupOfOrderDetails.GroupKey, Sales = groupOfOrderDetails.List.Items.Sum(a => a.SaleByOrderDetail) }).
                Sort(SortExpressionComparer<SalesByCountry>.Ascending(a => a.Sales)).
                ObserveOnDispatcher().
                Bind(out _salesByCountries);

            salesByCategories = orderDetailsList.Connect().
                Transform(orderDetail => new { Category = orderDetail.Product.Category.CategoryName, SaleByOrderDetail = orderDetail.UnitPrice * orderDetail.Quantity }).
                GroupOn(orderDetail => orderDetail.Category).
                Transform(groupOfOrderDeatils => new SalesByCategory() { Category = groupOfOrderDeatils.GroupKey, Sales = groupOfOrderDeatils.List.Items.Sum(a => a.SaleByOrderDetail) }).
                ObserveOnDispatcher().
                Bind(out _salesByCategories);
            #endregion

            FillCollections();
        }

        private async void FillCollections()
        {
            var orderDetails = await northwindRepository.GetOrderDetails();

            var customers = await northwindRepository.GetCustomers();
            var products = await northwindRepository.GetProducts();
            var orders = await northwindRepository.GetOrders();
            var categories = await northwindRepository.GetCategories();
            var employees = await northwindRepository.GetEmployees();

            customersList.AddRange(customers);
            productsList.AddRange(products);
            ordersList.AddRange(orders);
            orderDetailsList.AddRange(orderDetails);

            //Filling navigation propertie(Category) of Products
            await Task.Run(() =>
            {
                products.ForEach(product =>
                {
                    product.Category = categories.First(category => category.CategoryID == product.CategoryID);
                });
            });

            //Filling naviagtion properties(Customer and Employee) of Order
            await Task.Run(() =>
            {
                orders.ForEach(order =>
                {
                    order.Customer = customers.First(customer => customer.CustomerID == order.CustomerID);
                    order.Employee = employees.First(employee => employee.EmployeeID == order.EmployeeID);
                });
            });

            //Filling naviagtion properties(Order and Product) of Order details
            await Task.Run(() =>
            {
                orderDetails.ForEach(orderDetail =>
                {
                    orderDetail.Order = orders.First(order => order.OrderID == orderDetail.OrderID);
                    orderDetail.Product = products.First(product => product.ProductID == orderDetail.ProductID);
                });
            });

            //Starting from this moment we track changes in source lists (i.e. customersList, productsList, ordersList, orderDetailsList)
            customersByCountries.Subscribe();
            purchasesByCustomers.Subscribe();
            salesByEmployees.Subscribe();
            productsByCategories.Subscribe();
            ordersByCountries.Subscribe();
            salesByCountries.Subscribe();
            salesByCategories.Subscribe();
        }

        #region Customers statistics
        public async Task<IEnumerable<CustomersByCountry>> GetCustomersByCountries()
        {
            return await Task.FromResult(_customersByCountries);
        }

        public async Task<IEnumerable<PurchasesByCustomers>> GetPurchasesByCustomers()
        {
            return await Task.FromResult(_purchasesByCustomers);
        }
        #endregion

        #region Employees statistics
        public async Task<IEnumerable<EmployeeSales>> GetSalesByEmployees()
        {
            return await Task.FromResult(_salesByEmployees);
        }
        #endregion

        #region Products statstics
        public async Task<IEnumerable<ProductsByCateogries>> GetProductsByCategories()
        {
            return await Task.FromResult(_productsByCategories);
        }
        #endregion

        #region Orders statstics
        public async Task<IEnumerable<OrdersByCountry>> GetOrdersByCountries()
        {
            return await Task.FromResult(_ordersByCountries);
        }

        public async Task<IEnumerable<SalesByCategory>> GetSalesByCategories()
        {
            return await Task.FromResult(_salesByCategories);
        }

        public async Task<IEnumerable<SalesByCountry>> GetSalesByCountries()
        {
            return await Task.FromResult(_salesByCountries);
        }

        public async Task<string> GetSummary(string summary)
        {
            string result = null;

            //Waits until order details will be filled 
            //TODO: Find another algorithm of waiting for filling of the collection 
            await Task.Run(() =>
            {
                while (orderDetailsList.Count == 0) { }
            });

            switch (summary)
            {
                case "OverallSalesSum":
                    await Task.Run(() =>
                    {
                        result = orderDetailsList.Items.Sum(orderDetail => orderDetail.UnitPrice * orderDetail.Quantity).ToString(format);
                    });
                    break;
                case "MaxCheck":
                    await Task.Run(() =>
                    {
                        result = orderDetailsList.Items.GroupBy(orderDetail => orderDetail.OrderID).
                                                        Select(groupOfOrderDetails => new { SalesByOrder = groupOfOrderDetails.Sum(orderDetail => orderDetail.Quantity * orderDetail.UnitPrice) }).
                                                        Max(a => a.SalesByOrder).
                                                        ToString(format);
                    });
                    break;
                case "MinCheck":
                    await Task.Run(() =>
                    {
                        result = orderDetailsList.Items.GroupBy(orderDetail => orderDetail.OrderID).
                                Select(groupOfOrderDetails => new { SalesByOrder = groupOfOrderDetails.Sum(orderDetail => orderDetail.Quantity * orderDetail.UnitPrice) }).
                                Min(a => a.SalesByOrder).
                                ToString(format);
                    });
                    break;
                case "AverageCheck":
                    await Task.Run(() =>
                    {
                        result = orderDetailsList.Items.GroupBy(orderDetail => orderDetail.OrderID).
                                Select(groupOfOrderDetails => new { SalesByOrder = groupOfOrderDetails.Sum(orderDetail => orderDetail.Quantity * orderDetail.UnitPrice) }).
                                Average(a => a.SalesByOrder).
                                ToString(format);
                    });
                    break;
                case "OrdersQuantity":
                    await Task.Run(() =>
                    {
                        result = ordersList.Count.ToString();
                    });
                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }

        string format = "$ ###,###.###";
        #endregion
    }
}
