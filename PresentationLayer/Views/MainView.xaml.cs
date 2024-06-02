using System.Windows;
using TourPlanner.PresentationLayer.ViewModels;

namespace TourPlanner.PresentationLayer.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = IoCContainerConfig.Instance.MainViewModel;
            ((BaseViewModel)DataContext).OnRequestClose += (s, e) => Application.Current.Shutdown();
        }
    }
}