using Medicine.Data;
using System;
using System.Windows;

namespace Medicine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DataContext Context = new DataContext();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var dataContext = new DataContext();
            dataContext.Database.Initialize(true);
        }

        [STAThread]
        static void Main()
        {
            try
            {
                App app = new App();
                MainWindow window = new MainWindow();
                app.Run(window);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
