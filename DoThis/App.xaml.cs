using System.Windows;
using Beeffective.Bootstrap;
using Beeffective.Views;
using Syncfusion.Licensing;

namespace Beeffective
{
    public partial class App
    {
        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense("##SyncfusionLicense##");
        }

        private static AppContainer container;
        internal static AppContainer Container => container ??= new AppContainer();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = Container.Resolve<HoneycombWindow>();
            window.Show();
        }
    }
}
