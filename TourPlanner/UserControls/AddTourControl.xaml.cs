using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TourPlanner.UserControls
{
    /// <summary>
    /// Interaction logic for AddTourControl.xaml
    /// </summary>
    public partial class AddTourControl : UserControl
    {
        public AddTourControl()
        {
            InitializeComponent();
            InitializeAsync();
            IoCContainerConfig.Instance.MainViewModel.ViewModel.WebViewRefreshEventHandler += WebViewRefreshEvent;
        }

        private void WebViewRefreshEvent(object? sender, EventArgs? args)
        {
            webView.Reload();
        }

        private async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = System.IO.Path.Combine(appDir, "Resources/WebView/leaflet.html");
            webView.CoreWebView2.Navigate(filePath);
        }
    }
}
