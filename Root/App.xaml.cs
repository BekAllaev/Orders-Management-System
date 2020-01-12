using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Prism;
using Syncfusion.Licensing;
using System.Windows;

namespace Root
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Bootstrapper bootstrapper;

        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense("MTk1NDgyQDMxMzcyZTM0MmUzMEVRRDhDWENqQ2t6aGM0YUZPbkJkNHk3YmFndWpxbWlyZ2RGSnBtRkd0Q1k9");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
