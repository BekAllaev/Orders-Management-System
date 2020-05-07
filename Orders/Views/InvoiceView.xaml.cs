using System;
using System.Collections.Generic;
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
using System.IO;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive;
using ReactiveUI;
using Orders.ViewModels;
using Orders.Events;

namespace Orders.Views
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class InvoiceView : UserControl
    {
        public InvoiceView()
        {
            InitializeComponent();

            MessageBus.Current.Listen<OrerDataCreated>()
                .Subscribe(orderCreatedEvent => SetDataSources());
        }

        private void SetDataSources()
        {
            this.InvoiceReport.DataSources.Clear();

            var orders = (this.DataContext as InvoiceViewModel).Orders;
            var orderDetails = (this.DataContext as InvoiceViewModel).OrderDetails;

            this.InvoiceReport.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource()
            {
                Name = "Orders",
                Value = orders
            });

            this.InvoiceReport.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource()
            {
                Name = "OrderDetails",
                Value = orderDetails
            });

            this.InvoiceReport.RefreshReport();
        }
    }
}
