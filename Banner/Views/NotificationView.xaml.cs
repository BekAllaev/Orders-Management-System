using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;

namespace Banner.Views
{
    /// <summary>
    /// Interaction logic for MessagesView.xaml
    /// </summary>
    public partial class NotificationView : UserControl
    {
        public NotificationView()
        {
            InitializeComponent();
        }

        private async void TextBlock_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            if (string.IsNullOrWhiteSpace(textBlock.Text)) return;

            packIcon.Kind = PackIconKind.Error;
            packIcon.Foreground = new SolidColorBrush(Colors.Red);

            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(500);
                packIcon.Visibility = Visibility.Hidden;
                await Task.Delay(500);
                packIcon.Visibility = Visibility.Visible;
            }
        }
    }
}
