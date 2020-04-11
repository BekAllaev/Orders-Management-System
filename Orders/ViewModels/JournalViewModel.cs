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
using System.Reactive.Linq;
using Infrastructure.Extensions;
using System.Reactive;
using System.Data.Common;
using System.Data.Entity.Core;

namespace Orders.ViewModels
{
    public class JournalViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations
        NorthwindContext northwindContext;

        SourceList<Order> ordersList;

        ReadOnlyObservableCollection<Order> _ordersList;

        readonly IEnumerable<Order> cachedCollection;

        string _searchTerm;
        #endregion

        #region Construct
        public JournalViewModel(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;

            ordersList = new SourceList<Order>();

            ordersList.Connect().
                ObserveOnDispatcher().
                Bind(out _ordersList).
                Subscribe();

            cachedCollection = northwindContext.Orders;

            this.WhenAnyValue(x => x.SearchTerm).
                Subscribe(async newSearchTerm =>
                {
                    if (newSearchTerm != null)
                        if (string.IsNullOrEmpty(newSearchTerm)) await FillOrderList(cachedCollection.ToList());
                        else
                        {
                            var filteredList = cachedCollection.Where(o => o.CustomerID.SafeSubstring(0, newSearchTerm.Length).ToLower() == newSearchTerm.ToLower()).OrderBy(o => o.CustomerID).ToList();

                            await FillOrderList(filteredList);
                        }
                });

            ClearSearchBoxCommand = ReactiveCommand.Create(ClearSearchBoxExecute);
        }
        #endregion

        #region Utilities
        /// <summary>
        /// First clears collection of orders than fills it with new collection
        /// </summary>
        /// <param name="currentOrderList">
        /// Current orders collection
        /// </param>
        /// <param name="listToFill">
        /// List which must be added into orders collection
        /// </param>
        async Task FillOrderList(List<Order> listToFill)
        {
            var currentOrdersList = ordersList.Items;

            await Task.Run(() =>
            {
                foreach (var order in currentOrdersList)
                    ordersList.Remove(order);

                foreach (var order in listToFill)
                    ordersList.Add(order);
            });
        }
        #endregion

        #region Properties
        public ReadOnlyObservableCollection<Order> Orders => _ordersList;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { this.RaiseAndSetIfChanged(ref _searchTerm, value); }
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
                if (ordersList.Count == 0) await Task.Run(() => { ordersList.AddRange(northwindContext.Orders); });
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
