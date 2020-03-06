using DataAccessLocal;
using Syncfusion.UI.Xaml.Controls.DataPager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Orders.Views
{
    /// <summary>
    /// Interaction logic for JournalView.xaml
    /// </summary>
    public partial class JournalView : UserControl
    {
        NorthwindContext northwindContext;
        List<Order> orders;

        public JournalView(NorthwindContext northwindContext)
        {
            InitializeComponent();

            this.northwindContext = northwindContext;

            dataPager.OnDemandLoading += DataPager_OnDemandLoading;
        }

        private async void DataPager_OnDemandLoading(object sender, Syncfusion.UI.Xaml.Controls.DataPager.OnDemandLoadingEventArgs e)
        {
            var dataPager = (SfDataPager)sender;

            if (orders == null)
                await Task.Run(() =>
                {
                    orders = northwindContext.Orders.ToList();
                });

            dataPager.LoadDynamicItems(e.StartIndex, orders.GetRange(e.StartIndex, e.PageSize));

            var a = dataPager.PagedSource;
        }
    }
}
