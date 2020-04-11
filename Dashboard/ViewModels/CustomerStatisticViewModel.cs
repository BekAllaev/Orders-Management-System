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
using System.Data.Common;

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

            orderDetailsList.Connect().
                Transform(orderDetails => new { CompanyName = orderDetails.Order.Customer.CompanyName, PurchaseByOrderDetail = orderDetails.UnitPrice * orderDetails.Quantity }).
                GroupOn(orderDetails => orderDetails.CompanyName).
                Transform(groupOfOrderDetails => new PurchasesByCustomers() { CompanyName = groupOfOrderDetails.GroupKey, Purchases = groupOfOrderDetails.List.Items.Sum(a => a.PurchaseByOrderDetail) }).
                Sort(SortExpressionComparer<PurchasesByCustomers>.Descending(a => a.Purchases)).
                Top(10).
                ObserveOnDispatcher().
                Bind(out _purchases).
                Subscribe();
        }
        #endregion

        #region Implementation of IRegionMemberLifetime
        public bool KeepAlive => true;
        #endregion

        #region Implementation of INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                if (customersList.Count == 0) await Task.Run(() => customersList.AddRange(northwindContext.Customers));
                if (orderDetailsList.Count == 0) await Task.Run(() => orderDetailsList.AddRange(northwindContext.Order_Details));
            }
            catch(DbException e)
            {
                MessageBus.Current.SendMessage(e);
            }
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
    /// Company and purchases made by company
    /// </summary>
    public class PurchasesByCustomers
    {
        public string CompanyName { set; get; }

        public decimal Purchases { set; get; }
    }
    #endregion
}
