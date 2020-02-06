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

namespace Infrastructure.UserControls
{
    /// <summary>
    /// Interaction logic for UpDownControl.xaml
    /// </summary>
    public partial class UpDownControl : UserControl
    {
        #region Declarations
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty CurrentValueProperty;
        #endregion

        #region Constructor
        static UpDownControl()
        {
            ItemsSourceProperty = DependencyProperty.Register(
                "ItemsSource",
                typeof(List<int>),
                typeof(UpDownControl),
                new FrameworkPropertyMetadata(
                    new List<int>(),
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(SetInitialCurrentValue),
                    null));

            CurrentValueProperty = DependencyProperty.Register(
                "CurrentValue",
                typeof(int),
                typeof(UpDownControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    null));
        }

        public UpDownControl()
        {
            InitializeComponent();

            Binding binding = new Binding()
            {
                Source = this,
                Path = new PropertyPath("CurrentValue"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            this.CurrentValueTB.SetBinding(TextBlock.TextProperty, binding);
        }
        #endregion

        #region Properties
        public List<int> ItemsSource
        {
            get { return (List<int>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public int CurrentValue
        {
            get { return (int)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }
        #endregion

        #region Event handlers
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentValue == ItemsSource[ItemsSource.Count - 1]) return;

            CurrentValue++;
        }
        private void CurrentValueTB_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (CurrentValue != ItemsSource[ItemsSource.Count - 1] && e.Delta > 0)
                CurrentValue++;
            else if (CurrentValue != ItemsSource[0] && e.Delta < 0)
                CurrentValue--;
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentValue == ItemsSource[0]) return;

            CurrentValue--;
        }
        #endregion

        #region Utilities
        private static void SetInitialCurrentValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sourceList = (List<int>)e.NewValue;
            int initialValue = sourceList[0];
            d.SetValue(CurrentValueProperty, initialValue);
        }
        #endregion
    }
}
