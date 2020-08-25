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
using OMS.Data;
using OMS.WPFClient.Infrastructure.Services.StatisticService;

namespace OMS.WPFClient.Modules.Dashboard.ViewModels
{
    public class ProductStatisticViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declaration
        INorthwindRepository northwindRepository;
        IStatisticService statisticService;
        #endregion

        #region Constructor
        public ProductStatisticViewModel(INorthwindRepository northwindRepository, IStatisticService statisticService)
        {
            this.northwindRepository = northwindRepository;
            this.statisticService = statisticService;
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
                CateogrySummaries = await statisticService.GetProductsByCategories();
            }
            catch (DbException e)
            {
                MessageBus.Current.SendMessage(e);
            }
        }
        #endregion

        #region Properties
        IEnumerable<ProductsByCateogries> _categorySummaries;
        public IEnumerable<ProductsByCateogries> CateogrySummaries 
        { 
            get { return _categorySummaries; }
            set { this.RaiseAndSetIfChanged(ref _categorySummaries, value); }
        }
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
