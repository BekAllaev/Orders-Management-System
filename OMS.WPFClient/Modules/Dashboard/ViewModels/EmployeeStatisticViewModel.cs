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
using OMS.WPFClient.Infrastructure.Services.StatisticService;
using System.Collections.Generic;
using Syncfusion.Data.Extensions;
using System.Net.Http;

namespace OMS.WPFClient.Modules.Dashboard.ViewModels
{
    public class EmployeeStatisticViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations
        INorthwindRepository northwindRepository;
        IStatisticService statisticService;
        #endregion

        #region Constructor
        public EmployeeStatisticViewModel(INorthwindRepository northwindRepository, IStatisticService statisticService)
        {
            this.northwindRepository = northwindRepository;
            this.statisticService = statisticService;
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
                Employees = await statisticService.GetSalesByEmployees();
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
        IEnumerable<EmployeeSales> _employeeSales;
        public IEnumerable<EmployeeSales> Employees
        {
            get { return _employeeSales; }
            set { this.RaiseAndSetIfChanged(ref _employeeSales, value); }
        }
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
