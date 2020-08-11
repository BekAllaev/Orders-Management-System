using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Prism.Regions;
using OMS.DataAccessLocal;
using System.Collections.ObjectModel;
using DynamicData;
using ReactiveUI;
using DynamicData.Binding;
using OMS.Data.Models;
using System.Data.Common;
using OMS.Data;

namespace OMS.WPFClient.Modules.Dashboard.ViewModels
{
    public class EmployeeStatisticViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations
        INorthwindRepository northwindRepository;

        ReadOnlyObservableCollection<EmployeeSales> _employees;

        SourceList<Order_Detail> orderDetailsList;
        #endregion

        #region Constructor
        public EmployeeStatisticViewModel(INorthwindRepository northwindRepository)
        {
            this.northwindRepository = northwindRepository;

            orderDetailsList = new SourceList<Order_Detail>();

            orderDetailsList.Connect().
                Transform(orderDetail => new { LastName = orderDetail.Order.Employee.LastName, SaleByOrderDetail = orderDetail.UnitPrice * orderDetail.Quantity }).
                GroupOn(orderDetail => orderDetail.LastName).
                Transform(groupOfOrderDetail => new EmployeeSales() { LastName = groupOfOrderDetail.GroupKey, Sales = groupOfOrderDetail.List.Items.Sum(a => a.SaleByOrderDetail) }).
                ObserveOnDispatcher().
                Sort(SortExpressionComparer<EmployeeSales>.Ascending(a => a.Sales)).
                Bind(out _employees).
                Subscribe();
        }
        #endregion

        #region IRegionMemberLifetime implementation
        public bool KeepAlive => true;
        #endregion

        #region INavigationAware implementation
        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                if (orderDetailsList.Count == 0)
                {
                    var orderDetails = await northwindRepository.GetOrderDetails();
                    orderDetailsList.AddRange(orderDetails);
                }
            }
            catch (DbException e)
            {
                MessageBus.Current.SendMessage(e);
            }
        }
        #endregion

        #region Properties
        public ReadOnlyObservableCollection<EmployeeSales> Employees => _employees;
        #endregion
    }

    #region Screen object
    /// <summary>
    /// Sales made by employee
    /// </summary>
    public class EmployeeSales
    {
        public string LastName { set; get; }
        public decimal Sales { set; get; }
    }
    #endregion
}
