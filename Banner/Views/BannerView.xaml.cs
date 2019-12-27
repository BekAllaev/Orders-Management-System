﻿using System;
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
    /// Interaction logic for BannerView.xaml
    /// </summary>
    public partial class BannerView : UserControl
    {
        public BannerView()
        {
            InitializeComponent();
        }

        public void ChangeSettingButtonIsChecked()
        {
            SettingsToggleButton.IsChecked = SettingsToggleButton.IsChecked == true ? false : true;
        }
    }
}
