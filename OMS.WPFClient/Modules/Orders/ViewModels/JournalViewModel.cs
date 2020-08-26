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
using OMS.DataAccessLocal;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using OMS.WPFClient.Infrastructure.Extensions;
using System.Reactive;
using System.Data.Common;
using System.Data.Entity.Core;
using OMS.Data.Models;
using OMS.WPFClient.Modules.Orders.Events;
using OMS.Data;

namespace OMS.WPFClient.Modules.Orders.ViewModels
{
    public class JournalViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations
        INorthwindRepository northwindRepository;

        IEnumerable<Order> cachedCollection;

        string _searchTerm;
        Order _order;
        #endregion

        #region Construct
        public JournalViewModel(INorthwindRepository northwindRepository)
        {
            this.northwindRepository = northwindRepository;

            this.WhenAnyValue(x => x.SearchTerm).
                Subscribe(newSearchTerm =>
                {
                    if (newSearchTerm != null)
                        if (string.IsNullOrEmpty(newSearchTerm)) Orders = cachedCollection;
                        else
                        {
                            var filteredList = cachedCollection.Where(o => o.CustomerID.SafeSubstring(0, newSearchTerm.Length).ToLower() == newSearchTerm.ToLower()).OrderBy(o => o.CustomerID).ToList();

                            Orders = filteredList;
                        }
                });

            ClearSearchBoxCommand = ReactiveCommand.Create(ClearSearchBoxExecute);
        }
        #endregion

        #region Properties
        IEnumerable<Order> _orders;
        public IEnumerable<Order> Orders 
        { 
            get { return _orders; }
            set { this.RaiseAndSetIfChanged(ref _orders, value); } 
        }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { this.RaiseAndSetIfChanged(ref _searchTerm, value); }
        }

        public Order SelectedOrder
        {
            get { return _order; }
            set
            {
                if (value != null)
                    MessageBus.Current.SendMessage<NewOrderCreated>(new NewOrderCreated() { OrderId = value.OrderID });
            }
        }
        #endregion

        #region Implementation of IRegionMemberLifetime
        public bool KeepAlive => true;
        #endregion

        #region Implementation of INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext) { return true; }

        public void OnNavigatedFrom(NavigationContext navigationContext) { }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                cachedCollection = await northwindRepository.GetOrders();
                Orders = cachedCollection;
            }
            catch (Exception e)
            {
                if (e is EntityCommandCompilationException || e is DbException)
                    MessageBus.Current.SendMessage(e);
            }
        }
        #endregion

        #region Commands
        public ReactiveCommand<Unit, Unit> ClearSearchBoxCommand { get; }

        private void ClearSearchBoxExecute()
        {
            SearchTerm = string.Empty;
        }
        #endregion
    }
}
