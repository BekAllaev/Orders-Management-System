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
using OMS.Data.Models;
using System.Data.Common;

namespace OMS.WPFClient.Modules.Dashboard.ViewModels
{
    public class ProductStatisticViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declaration
        NorthwindContext northwindContext;

        SourceList<Product> productsList;

        ReadOnlyObservableCollection<ProductsByCateogries> _categorySummaries;
        #endregion

        #region Constructor
        public ProductStatisticViewModel(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;

            productsList = new SourceList<Product>();

            productsList.Connect().
                GroupOn(product => product.Category.CategoryName).
                Transform(groupOfProducts => new ProductsByCateogries() { CategoryName = groupOfProducts.GroupKey, NumberOfProducts = groupOfProducts.List.Count }).
                ObserveOnDispatcher().
                Bind(out _categorySummaries).
                Subscribe();
        }
        #endregion

        #region Implementation of IRegionMemberLifeTime
        public bool KeepAlive => true;
        #endregion

        #region Implementation of INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext) { return true; }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                if (productsList.Count == 0) await Task.Run(() => productsList.AddRange(northwindContext.Products));
            }
            catch(DbException e)
            {
                MessageBus.Current.SendMessage(e);
            }
        }
        #endregion

        #region Properties
        public ReadOnlyObservableCollection<ProductsByCateogries> CateogrySummaries => _categorySummaries;
        #endregion
    }

    #region Screen object
    /// <summary>
    /// Category name and number of products representative this category
    /// </summary>
    public class ProductsByCateogries
    {
        public string CategoryName { set; get; }

        public int NumberOfProducts { set; get; }
    }
    #endregion
}
