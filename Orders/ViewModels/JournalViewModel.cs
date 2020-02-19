using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using DynamicData;
using DataAccessLocal;
using System.Collections.ObjectModel;

namespace Orders.ViewModels
{
    public class JournalViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations
        NorthwindContext northwindContext;

        SourceList<Order> ordersList;

        ReadOnlyObservableCollection<Order> _ordersList;
        #endregion

        #region Construct
        public JournalViewModel(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;

            ordersList = new SourceList<Order>();

            ordersList.Connect().
                Bind(out _ordersList).
                Subscribe();
        }
        #endregion

        #region Properties
        public ReadOnlyObservableCollection<Order> Orders => _ordersList;
        #endregion

        #region Implementation of IRegionMemberLifetime
        public bool KeepAlive => true;
        #endregion

        #region Implementation of INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) { }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await Task.Run(() => { ordersList.AddRange(northwindContext.Orders); });
        }
        #endregion

        #region Commands
        #endregion
    }
}
