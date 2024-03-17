using System.Configuration;
using System.Data;
using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var addNewTourViewModel = new ViewModelAddNewTour();

            var wnd = new MainWindow
            {
                DataContext = new ViewModel(addNewTourViewModel),
                AddTourWindow = { DataContext = addNewTourViewModel }
            };

            wnd.Show();
        }
    }

}
