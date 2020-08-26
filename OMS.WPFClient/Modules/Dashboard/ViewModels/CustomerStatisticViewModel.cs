using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Prism.Regions;
using OMS.DataAccessLocal;
using System.Collections.ObjectModel;
using DynamicData;
using ReactiveUI;
using OMS.Data.Models;
using DynamicData.Binding;
using System.Data.Common;
using OMS.Data;
using OMS.WPFClient.Infrastructure.Services.StatisticService;
using System.Net.Http;

namespace OMS.WPFClient.Modules.Dashboard.ViewModels
{
    public class CustomerStatisticViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations
        INorthwindRepository northwindRepository;
        IStatisticService statisticService;
        #endregion

        #region Constructor
        public CustomerStatisticViewModel(INorthwindRepository northwindRepository, IStatisticService statisticService)
        {
            this.northwindRepository = northwindRepository;
            this.statisticService = statisticService;
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
                Customers = await statisticService.GetCustomersByCountries();
                Purchases = await statisticService.GetPurchasesByCustomers();
            }
            catch (Exception e)
            {
                if (e is DbException)
                    MessageBus.Current.SendMessage(e);
                else if (e is HttpRequestException)
                    MessageBus.Current.SendMessage<HttpRequestException>(new HttpRequestException(e.Message, e.InnerException));
            }
        }
        #endregion

        #region Properties
        IEnumerable<CustomersByCountry> _customersByCountry; 
        public IEnumerable<CustomersByCountry> Customers 
        { 
            get { return _customersByCountry; }
            set { this.RaiseAndSetIfChanged(ref _customersByCountry, value); }
        }

        IEnumerable<PurchasesByCustomers> _purchasesByCustomers;
        public IEnumerable<PurchasesByCustomers> Purchases 
        { 
            get { return _purchasesByCustomers; }
            set { this.RaiseAndSetIfChanged(ref _purchasesByCustomers, value); }
        }
        #endregion
    }

    #region Screen object
    /// <summary>
    /// Country and number of customers representative this country
    /// </summary>
    public class CustomersByCountry
    {
        public string CountryName { set; get; }

        public int CustomersCount { set; get; }
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
