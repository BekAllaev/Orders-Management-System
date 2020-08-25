using DynamicData;
using DynamicData.Binding;
using OMS.Data.Models;
using OMS.DataAccessLocal;
using OMS.WPFClient.Modules.Dashboard.ViewModels;
using ReactiveUI;
using Syncfusion.Data.Extensions;
using Syncfusion.Linq;
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
    public class StatisticLocalService : ReactiveObject, IStatisticService
    {
        #region Declarations
        NorthwindContext northwindContext;

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

        List<Order_Detail> orderDetails;
        #endregion

        public StatisticLocalService(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;

            productsList = new SourceList<Product>();
            customersList = new SourceList<Customer>();
            orderDetailsList = new SourceList<Order_Detail>();
            ordersList = new SourceList<Order>();
            orderDetails = northwindContext.Order_Details.ToList();

            #region Customers statistics
            customersList.Connect().
                GroupOn(customer => customer.Country).
                Transform(customersGroup => new CustomersByCountry() { CountryName = customersGroup.GroupKey, CustomersCount = customersGroup.List.Count }).
                ObserveOnDispatcher().
                Bind(out _customersByCountries).
                Subscribe();

            orderDetailsList.Connect().
                Transform(orderDetail => new { CompanyName = orderDetail.Order.Customer.CompanyName, PurchaseByOrderDetail = orderDetail.UnitPrice * orderDetail.Quantity }).
                GroupOn(orderDetail => orderDetail.CompanyName).
                Transform(groupOfOrderDetails => new PurchasesByCustomers() { CompanyName = groupOfOrderDetails.GroupKey, Purchases = groupOfOrderDetails.List.Items.Sum(a => a.PurchaseByOrderDetail) }).
                Sort(SortExpressionComparer<PurchasesByCustomers>.Descending(a => a.Purchases)).
                Top(10).
                ObserveOnDispatcher().
                Bind(out _purchasesByCustomers).
                Subscribe();
            #endregion

            #region Employees statistics
            orderDetailsList.Connect().
                Transform(orderDetail => new { LastName = orderDetail.Order.Employee.LastName, SaleByOrderDetail = orderDetail.UnitPrice * orderDetail.Quantity }).
                GroupOn(orderDetail => orderDetail.LastName).
                Transform(groupOfOrderDetail => new EmployeeSales() { LastName = groupOfOrderDetail.GroupKey, Sales = groupOfOrderDetail.List.Items.Sum(a => a.SaleByOrderDetail) }).
                ObserveOnDispatcher().
                Sort(SortExpressionComparer<EmployeeSales>.Ascending(a => a.Sales)).
                Bind(out _salesByEmployees).
                Subscribe();
            #endregion

            #region Products statistics
            productsList.Connect().
                GroupOn(product => product.Category.CategoryName).
                Transform(groupOfProducts => new ProductsByCateogries() { CategoryName = groupOfProducts.GroupKey, NumberOfProducts = groupOfProducts.List.Count }).
                ObserveOnDispatcher().
                Bind(out _productsByCategories).
                Subscribe();
            #endregion

            #region Orders statistics
            ordersList.Connect().
                GroupOn(order => order.Customer.Country).
                Transform(groupOfOrders => new OrdersByCountry() { Country = groupOfOrders.GroupKey, NumberOfOrders = groupOfOrders.List.Count }).
                ObserveOnDispatcher().
                Top(10).
                Bind(out _ordersByCountries).
                Subscribe();

            orderDetailsList.Connect().Transform(orderDetail => new { Country = orderDetail.Order.Customer.Country, SaleByOrderDetail = orderDetail.UnitPrice * orderDetail.Quantity }).
                GroupOn(orderDetail => orderDetail.Country).
                Transform(groupOfOrderDetails => new SalesByCountry() { Country = groupOfOrderDetails.GroupKey, Sales = groupOfOrderDetails.List.Items.Sum(a => a.SaleByOrderDetail) }).
                Sort(SortExpressionComparer<SalesByCountry>.Ascending(a => a.Sales)).
                ObserveOnDispatcher().
                Bind(out _salesByCountries).
                Subscribe();

            orderDetailsList.Connect().
                Transform(orderDetail => new { Category = orderDetail.Product.Category.CategoryName, SaleByOrderDetail = orderDetail.UnitPrice * orderDetail.Quantity }).
                GroupOn(orderDetail => orderDetail.Category).
                Transform(groupOfOrderDeatils => new SalesByCategory() { Category = groupOfOrderDeatils.GroupKey, Sales = groupOfOrderDeatils.List.Items.Sum(a => a.SaleByOrderDetail) }).
                ObserveOnDispatcher().
                Bind(out _salesByCategories).
                Subscribe();

            OverallSalesSum = orderDetails.Sum(orderDetail => orderDetail.UnitPrice * orderDetail.Quantity).ToString(format);

            var ordersChecks = orderDetails.GroupBy(orderDetail => orderDetail.OrderID).
                Select(groupOfOrderDetails => new { SalesByOrder = groupOfOrderDetails.Sum(orderDetail => orderDetail.Quantity * orderDetail.UnitPrice) });

            MaxCheck = ordersChecks.Max(a => a.SalesByOrder).ToString(format);

            MinCheck = ordersChecks.Min(a => a.SalesByOrder).ToString(format);

            AverageCheck = ordersChecks.Average(a => a.SalesByOrder).ToString(format);

            OrdersQuantity = northwindContext.Orders.Count().ToString();

            #region This part of code uses ObservabelAsPropertyHelper class. 

            //TODO: Find out how to return value of ObservabelAsPropertyHelper to the view models

            //orderDetailsList.Connect().Select(a => orderDetailsList.Items.Select(orderDetail => orderDetail.Quantity * orderDetail.UnitPrice).Sum().ToString(format)).
            //    ToProperty(this, vm => vm.OverallSalesSum, out _overallSalesSum);

            //var orderDetails = orderDetailsList.Connect().
            //    Transform(orderDetail => new { OrderID = orderDetail.OrderID, SaleByOrderDetail = orderDetail.UnitPrice * orderDetail.Quantity }).
            //    GroupOn(orderDetail => orderDetail.OrderID).
            //    Transform(groupOfOrderDetails => new { OrderID = groupOfOrderDetails.GroupKey, SaleByOrder = groupOfOrderDetails.List.Items.Sum(a => a.SaleByOrderDetail) }).
            //    Publish();

            //orderDetails.Select(a => a.Last().Range.Min(b => b.SaleByOrder).ToString(format)).
            //    ToProperty(this, vm => vm.MinCheck, out _minCheck);

            //orderDetails.Select(a => a.Last().Range.Max(b => b.SaleByOrder).ToString(format)).
            //    ToProperty(this, vm => vm.MaxCheck, out _maxCheck);

            //orderDetails.Select(a => a.Last().Range.Average(b => b.SaleByOrder).ToString(format)).
            //    ToProperty(this, vm => vm.AverageCheck, out _averageCheck);

            //orderDetails.Select(a => a.Last().Range.Count.ToString()).
            //    ToProperty(this, vm => vm.OrdersQuantity, out _ordersQuantity);

            //orderDetails.Connect();
            #endregion
            #endregion

            FillCollections();
        }

        private async void FillCollections()
        {
            await Task.Run(() =>
            {
                customersList.AddRange(northwindContext.Customers.ToList());
                orderDetailsList.AddRange(northwindContext.Order_Details.ToList());
                productsList.AddRange(northwindContext.Products.ToList());
                ordersList.AddRange(northwindContext.Orders.ToList());
            });
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
            switch (summary)
            {
                case "OverallSalesSum":
                    return await Task.FromResult(OverallSalesSum);
                case "MaxCheck":
                    return await Task.FromResult(MaxCheck);
                case "MinCheck":
                    return await Task.FromResult(MinCheck);
                case "AverageCheck":
                    return await Task.FromResult(AverageCheck);
                case "OrdersQuantity":
                    return await Task.FromResult(OrdersQuantity);
                default:
                    throw new Exception();
            }
        }

        string format = "$ ###,###.###";

        #region This part of code uses ObservableAsPropertyHelper
        //readonly ObservableAsPropertyHelper<string> _overallSalesSum;
        //public string OverallSalesSum => _overallSalesSum.Value;

        //readonly ObservableAsPropertyHelper<string> _minCheck;
        //public string MinCheck => _minCheck.Value;

        //readonly ObservableAsPropertyHelper<string> _maxCheck;
        //public string MaxCheck => _maxCheck.Value;

        //readonly ObservableAsPropertyHelper<string> _averageCheck;
        //public string AverageCheck => _averageCheck.Value;

        //readonly ObservableAsPropertyHelper<string> _ordersQuantity;
        //public string OrdersQuantity => _ordersQuantity.Value;
        #endregion

        public string OverallSalesSum { get; }

        public string MaxCheck { get; }

        public string MinCheck { get; }

        public string AverageCheck { get; }

        public string OrdersQuantity { get; }
        #endregion
    }
}
