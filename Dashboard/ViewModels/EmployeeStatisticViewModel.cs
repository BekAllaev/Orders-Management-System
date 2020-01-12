using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Prism.Regions;
using DataAccessLocal;
using DataAccessLocal.Models;
using System.Collections.ObjectModel;
using DynamicData;

namespace Dashboard.ViewModels
{
    public class EmployeeStatisticViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations
        NorthwindContext northwindContext;

        ReadOnlyObservableCollection<Employee> _employees;

        SourceCache<Employee,int> employeesList;
        SourceCache<Order, int> ordersList;
        SourceList<Order_Detail> orderDetailsList;
        #endregion

        #region Constructor
        public EmployeeStatisticViewModel(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;

            employeesList = new SourceCache<Employee, int>(p => p.EmployeeID);
            ordersList = new SourceCache<Order, int>(p => p.OrderID);
            orderDetailsList = new SourceList<Order_Detail>();

            employeesList.Connect()
                .Bind(out _employees)
                .Subscribe();
        }
        #endregion

        #region IRegionMemberLifetime implementation
        public bool KeepAlive => true;
        #endregion

        #region INavigationAware implementation
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region Properties
        public ReadOnlyObservableCollection<Employee> Employees => _employees;
        #endregion
    }
}
