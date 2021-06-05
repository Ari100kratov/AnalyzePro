using Medicine.Data;
using System.Windows;

namespace Medicine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var dataContext = new DataContext();
            dataContext.Database.Initialize(true);
        }
    }
}
