using System.Windows;
using Beeffective.Data;
using Syncfusion.Licensing;

namespace Beeffective
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Database database;

        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense("##SyncfusionLicense##");
        }

        internal static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database();
                }
                return database;
            }
        }
    }
}
