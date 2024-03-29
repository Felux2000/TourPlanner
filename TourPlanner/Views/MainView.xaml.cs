using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourPlanner.ViewModels;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void btn_AddTour_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new AddTourViewModel(((MainViewModel)DataContext));
            AddTourView addTour = new AddTourView{
                DataContext = viewModel
        };
            viewModel.OnRequestClose += (s, e) => addTour.Close();
            addTour.Closed += AddTour_Closed;
            this.IsEnabled = false;
            addTour.Show();
        }
        private void AddTour_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
        }

        private void btn_EditTour_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new EditTourViewModel(((MainViewModel)DataContext));
            EditTourView editTour = new EditTourView{
                DataContext = viewModel
        };
            viewModel.OnRequestClose += (s, e) => editTour.Close();
            editTour.Closed += EditTour_Closed;
            this.IsEnabled = false;
            editTour.Show();
        }
        private void EditTour_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
        }

        private void btn_AddLog_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new AddTourLogViewModel(((MainViewModel)DataContext));
            AddTourLogView addLog = new AddTourLogView{
                DataContext = viewModel
        };
            viewModel.OnRequestClose += (s, e) => addLog.Close();
            addLog.Closed += AddLog_Closed;
            this.IsEnabled = false;
            addLog.Show();
        }
        private void AddLog_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
        }
    }
}