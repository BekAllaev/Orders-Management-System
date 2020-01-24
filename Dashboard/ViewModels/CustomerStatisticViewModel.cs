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
    public class CustomerStatisticViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations
        NorthwindContext northwindContext;

        SourceList<Customer> customersList;
        SourceList<Order_Detail> orderDetailsList;

        ReadOnlyObservableCollection<CustomersByCountry> _customers;
        ReadOnlyObservableCollection<PurchasesByCustomers> _purchases;
        #endregion

        #region Constructor
        public CustomerStatisticViewModel(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;

            customersList = new SourceList<Customer>();
            orderDetailsList = new SourceList<Order_Detail>();

            customersList.Connect().
                GroupOn(customer => customer.Country).
                Transform(customersGroup => new CustomersByCountry() { CountryName = customersGroup.GroupKey, NumberOfCustomers = customersGroup.List.Count }).
                ObserveOnDispatcher().
                Bind(out _customers).
                Subscribe();

            orderDetailsList.Connect()
                .Transform(orderDetails => new { CompanyName = orderDetails.Order.Customer.CompanyName, Purchase = orderDetails.UnitPrice * orderDetails.Quantity })
                .GroupOn(orderByCustomer => orderByCustomer.CompanyName)
                .Transform(groupOfOrders => new PurchasesByCustomers() { CompanyName = groupOfOrders.GroupKey, Purchases = groupOfOrders.List.Items.Sum(a => a.Purchase) })
                .Sort(SortExpressionComparer<PurchasesByCustomers>.Descending(a => a.Purchases))
                .Top(10)
                .ObserveOnDispatcher()
                .Bind(out _purchases)
                .Subscribe();
        }
        #endregion

        #region Implementation of IRegionMemberLifetime
        public bool KeepAlive => true;
        #endregion

        #region Implementation of INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (customersList.Count == 0) await Task.Run(() => customersList.AddRange(northwindContext.Customers));
            if (orderDetailsList.Count == 0) await Task.Run(() => orderDetailsList.AddRange(northwindContext.Order_Details));
        }
        #endregion

        #region Properties
        public ReadOnlyObservableCollection<CustomersByCountry> Customers => _customers;

        public ReadOnlyObservableCollection<PurchasesByCustomers> Purchases => _purchases;
        #endregion
    }

    #region Screen object
    /// <summary>
    /// Country and number of customers representative this country
    /// </summary>
    public class CustomersByCountry
    {
        public string CountryName { set; get; }

        public int NumberOfCustomers { set; get; }
    }

    /// <summary>
    /// Customer and purchases made by customer
    /// </summary>
    public class PurchasesByCustomers
    {
        public string CompanyName { set; get; }

        public decimal Purchases { set; get; }
    }
    #endregion
}
