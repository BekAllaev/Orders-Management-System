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
            SyncfusionLicenseProvider.RegisterLicense("MjUyNDAwQDMxMzgyZTMxMmUzMGdnbTVOTHJteFpkVUttczQra29uY01nUGNVenBGNloyd3RQczdaVzc5bkE9");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
