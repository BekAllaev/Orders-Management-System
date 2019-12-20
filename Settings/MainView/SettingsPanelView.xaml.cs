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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Settings.MainView
{
    /// <summary>
    /// Interaction logic for SettingsPanelView.xaml
    /// </summary>
    public partial class SettingsPanelView : UserControl
    {
        public SettingsPanelView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start animation of opening(or closing) settings panel
        /// </summary>
        public void StartAnimation()
        {
            DoubleAnimation da = new DoubleAnimation();

            if (SettingsPanel.Width != 0)
            {
                da.From = 300;
                da.To = 0;
            }
            else
            {
                da.From = 0;
                da.To = 300;
            }

            da.Duration = TimeSpan.FromSeconds(0.3);
            SettingsPanel.BeginAnimation(Border.WidthProperty, da);
        }
    }
}
