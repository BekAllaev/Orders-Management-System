using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Prism.Regions;
using DataAccessLocal;
using System.Collections.ObjectModel;
using DynamicData;
using ReactiveUI;
using DynamicData.Binding;

namespace Dashboard.ViewModels
{
    public class OrderStatisticViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations
        NorthwindContext northwindContext;

        SourceList<Order_Detail> orderDetailsList;
        SourceList<Order> ordersList;

        ReadOnlyObservableCollection<SalesByCountry> _salesByCountries;
        ReadOnlyObservableCollection<OrdersByCountry> _ordersByCountries;
        ReadOnlyObservableCollection<SalesByCategory> _salesByCategories;
        #endregion

        #region Constructor
        public OrderStatisticViewModel(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;

            orderDetailsList = new SourceList<Order_Detail>();
            ordersList = new SourceList<Order>();

            orderDetailsList.Connect().
                Transform(orderDetail => new { Country = orderDetail.Order.Customer.Country, Sale = orderDetail.UnitPrice * orderDetail.Quantity }).
                GroupOn(orderDetail => orderDetail.Country).
                Transform(groupOfOrderDetails => new SalesByCountry() { Country = groupOfOrderDetails.GroupKey, Sales = groupOfOrderDetails.List.Items.Sum(a => a.Sale) }).
                Sort(SortExpressionComparer<SalesByCountry>.Ascending(a => a.Sales)).
                ObserveOnDispatcher().
                Bind(out _salesByCountries).
                Subscribe();

            orderDetailsList.Connect()
                .Transform(orderDetail => new { Category = orderDetail.Product.Category.CategoryName, Sale = orderDetail.UnitPrice * orderDetail.Quantity })
                .GroupOn(orderDetail => orderDetail.Category)
                .Transform(groupOfOrderDeatils => new SalesByCategory() { Category = groupOfOrderDeatils.GroupKey, Sales = groupOfOrderDeatils.List.Items.Sum(a => a.Sale) })
                .ObserveOnDispatcher()
                .Bind(out _salesByCategories)
                .Subscribe();

            ordersList.Connect()
                .GroupOn(orders => orders.ShipCountry)
                .Transform(groupOfOrders => new OrdersByCountry() { Country = groupOfOrders.GroupKey, NumberOfOrders = groupOfOrders.List.Count })
                .ObserveOnDispatcher()
                .Top(10)
                .Bind(out _ordersByCountries)
                .Subscribe();

            orderDetailsList.Connect().
                Select(a => orderDetailsList.Items.Select(b => b.Quantity * b.UnitPrice).Sum().ToString(format)).
                ToProperty(this, vm => vm.OverallSalesSum, out _overallSalesSum);

            orderDetailsList.Connect()
                .Transform(orderDetail => new { OrderID = orderDetail.OrderID, Sale = orderDetail.UnitPrice * orderDetail.Quantity })
                .GroupOn(orderDetail => orderDetail.OrderID)
                .Transform(groupOfOrderDetails => new { OrderID = groupOfOrderDetails.GroupKey, SaleByOrder = groupOfOrderDetails.List.Items.Sum(a => a.Sale) })
                .Select(a => a.Last().Range.Min(b => b.SaleByOrder).ToString(format))
                .ToProperty(this, vm => vm.MinCheck, out _minCheck);

            orderDetailsList.Connect()
                .Transform(orderDetail => new { OrderID = orderDetail.OrderID, Sale = orderDetail.UnitPrice * orderDetail.Quantity })
                .GroupOn(orderDetail => orderDetail.OrderID)
                .Transform(groupOfOrderDetails => new { OrderID = groupOfOrderDetails.GroupKey, SaleByOrder = groupOfOrderDetails.List.Items.Sum(a => a.Sale) })
                .Select(a => a.Last().Range.Max(b => b.SaleByOrder).ToString(format))
                .ToProperty(this, vm => vm.MaxCheck, out _maxCheck);

            orderDetailsList.Connect()
                .Transform(orderDetail => new { OrderID = orderDetail.OrderID, Sale = orderDetail.UnitPrice * orderDetail.Quantity })
                .GroupOn(orderDetail => orderDetail.OrderID)
                .Transform(groupOfOrderDetails => new { OrderID = groupOfOrderDetails.GroupKey, SaleByOrder = groupOfOrderDetails.List.Items.Sum(a => a.Sale) })
                .Select(a => a.Last().Range.Average(b => b.SaleByOrder).ToString(format))
                .ToProperty(this, vm => vm.AverageCheck, out _averageCheck);

            orderDetailsList.Connect()
                .Transform(orderDetail => new { OrderID = orderDetail.OrderID, Sale = orderDetail.UnitPrice * orderDetail.Quantity })
                .GroupOn(orderDetail => orderDetail.OrderID)
                .Transform(groupOfOrderDetails => new { OrderID = groupOfOrderDetails.GroupKey, SaleByOrder = groupOfOrderDetails.List.Items.Sum(a => a.Sale) })
                .Select(a => a.Last().Range.Count.ToString())
                .ToProperty(this, vm => vm.OrdersQuantity, out _ordersQuantity);
        }
        #endregion

        #region Implementation of INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext) { return true; }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (orderDetailsList.Count == 0) await Task.Run(() => orderDetailsList.AddRange(northwindContext.Order_Details));
            if (ordersList.Count == 0) await Task.Run(() => ordersList.AddRange(northwindContext.Orders));
        }
        #endregion

        #region Implementation of IRegionMemeberLifetime 
        public bool KeepAlive => true;
        #endregion

        #region Properties
        public ReadOnlyObservableCollection<SalesByCountry> SalesByCountries => _salesByCountries;

        public ReadOnlyObservableCollection<OrdersByCountry> OrdersByCountries => _ordersByCountries;

        public ReadOnlyObservableCollection<SalesByCategory> SalesByCategories => _salesByCategories;

        #region Summaries
        string format = "$ ###,###.###";

        readonly ObservableAsPropertyHelper<string> _overallSalesSum;
        public string OverallSalesSum => _overallSalesSum.Value;

        readonly ObservableAsPropertyHelper<string> _minCheck;
        public string MinCheck => _minCheck.Value;

        readonly ObservableAsPropertyHelper<string> _maxCheck;
        public string MaxCheck => _maxCheck.Value;

        readonly ObservableAsPropertyHelper<string> _averageCheck;
        public string AverageCheck => _averageCheck.Value;

        readonly ObservableAsPropertyHelper<string> _ordersQuantity;
        public string OrdersQuantity => _ordersQuantity.Value;
        #endregion

        #endregion
    }

    #region Screen objects
    public class SalesByCountry
    {
        public string Country { set; get; }

        public decimal Sales { set; get; }
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
    #endregion
}
