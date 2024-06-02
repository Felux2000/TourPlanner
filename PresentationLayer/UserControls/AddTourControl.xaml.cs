using System.Windows.Controls;

namespace TourPlanner.PresentationLayer.UserControls
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
            if (webView.CoreWebView2 != null)
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
