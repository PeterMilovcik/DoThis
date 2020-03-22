using DoThis.Data;
using System.Windows;

namespace DoThis
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Database database;

        internal static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database();
                    database.Database.EnsureCreated();
                }
                return database;
            }
        }
    }
}
