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
using DynamicData.Binding;
using System.Data.Common;
using OMS.Data.Models;
using OMS.Data;
using OMS.WPFClient.Infrastructure.Services.StatisticService;
using System.Net.Http;

namespace OMS.WPFClient.Modules.Dashboard.ViewModels
{
    public class OrderStatisticViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations
        INorthwindRepository northwindRepository;
        IStatisticService statisticService;
        #endregion

        #region Constructor
        public OrderStatisticViewModel(INorthwindRepository northwindRepository, IStatisticService statisticService)
        {
            this.northwindRepository = northwindRepository;
            this.statisticService = statisticService;
        }
        #endregion

        #region Implementation of INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext) { return false; }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                SalesByCategories = await statisticService.GetSalesByCategories();
                SalesByCountries = await statisticService.GetSalesByCountries();
                OrdersByCountries = await statisticService.GetOrdersByCountries();

                OverallSalesSum = await statisticService.GetSummary("OverallSalesSum");
                MinCheck = await statisticService.GetSummary("MinCheck");
                MaxCheck = await statisticService.GetSummary("MaxCheck");
                AverageCheck = await statisticService.GetSummary("AverageCheck");
                OrdersQuantity = await statisticService.GetSummary("OrdersQuantity");
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

        #region Implementation of IRegionMemeberLifetime 
        public bool KeepAlive => true;
        #endregion

        #region Properties
        IEnumerable<SalesByCountry> _salesByCountries;
        public IEnumerable<SalesByCountry> SalesByCountries
        {
            get { return _salesByCountries; }
            set { this.RaiseAndSetIfChanged(ref _salesByCountries, value); }
        }

        IEnumerable<OrdersByCountry> _ordersByCountries;
        public IEnumerable<OrdersByCountry> OrdersByCountries
        {
            get { return _ordersByCountries; }
            set { this.RaiseAndSetIfChanged(ref _ordersByCountries, value); }
        }

        IEnumerable<SalesByCategory> _salesByCategories;
        public IEnumerable<SalesByCategory> SalesByCategories
        {
            get { return _salesByCategories; }
            set { this.RaiseAndSetIfChanged(ref _salesByCategories, value); }
        }

        #region Summaries
        string _overallSalesSum;
        public string OverallSalesSum
        {
            get { return _overallSalesSum; }
            set { this.RaiseAndSetIfChanged(ref _overallSalesSum, value); }
        }

        string _minCheck;
        public string MinCheck 
        { 
            get { return _minCheck; } 
            set { this.RaiseAndSetIfChanged(ref _minCheck, value); } 
        }

        string _maxCheck;
        public string MaxCheck 
        {
            get { return _maxCheck; }
            set { this.RaiseAndSetIfChanged(ref _maxCheck, value); }
        }

        string _averageCheck;
        public string AverageCheck 
        {
            get { return _averageCheck; }
            set { this.RaiseAndSetIfChanged(ref _averageCheck, value); }
        }

        string _ordersQuantity;
        public string OrdersQuantity 
        {
            get { return _ordersQuantity; }
            set { this.RaiseAndSetIfChanged(ref _ordersQuantity, value); }
        }
        #endregion

        #endregion
    }

    #region Screen objects
    /// <summary>
    /// Sales made to customers from one country
    /// </summary>
    public class SalesByCountry
    {
        public string Country { set; get; }

        public decimal Sales { set; get; }
    }

    /// <summary>
    /// Number of orders by customers in one country
    /// </summary>
    public class OrdersByCountry
    {
        public string Country { set; get; }

        public int NumberOfOrders { set; get; }
    }

    /// <summary>
    /// Sales which was made by selling products from category
    /// </summary>
    public class SalesByCategory
    {
        public string Category { set; get; }

        public decimal Sales { set; get; }
    }
    #endregion
}
