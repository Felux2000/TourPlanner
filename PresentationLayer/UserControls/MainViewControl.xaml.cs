using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Windows.Controls;

namespace TourPlanner.PresentationLayer.UserControls
{
    /// <summary>
    /// Interaction logic for MainViewControl.xaml
    /// </summary>
    public partial class MainViewControl : UserControl
    {
        public MainViewControl()
        {
            InitializeComponent();
            InitializeAsync();
            IoCContainerConfig.Instance.MainViewModel.WebViewRefreshEventHandler += WebViewRefreshEvent;
            IoCContainerConfig.Instance.MainViewModel.CaptureTourImageEvent += CaptureWebView;
        }

        private void WebViewRefreshEvent(object? sender, EventArgs? args)
        {
            if (webView.CoreWebView2 != null)
                webView.Reload();
        }

        private async Task<byte[]> CaptureWebView(object? sender, EventArgs? args)
        {
            try
            {
                dynamic clip = new JObject();
                clip.x = 0;
                clip.y = 0;
                clip.width = webView.ActualWidth;
                clip.height = webView.ActualHeight;
                clip.scale = 1;

                dynamic settings = new JObject();
                settings.format = "png";
                settings.clip = clip;
                settings.fromSurface = true;
                settings.captureBeyondViewport = false;

                string p = settings.ToString(Formatting.None);

                var devData = await webView.CoreWebView2.CallDevToolsProtocolMethodAsync("Page.captureScreenshot", p);

                var imgData = (string)((dynamic)JObject.Parse(devData)).data;
                return Convert.FromBase64String(imgData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return [];
            }
        }

        private async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async();
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = System.IO.Path.Combine(appDir, "Resources/WebView/leaflet.html");
            webView.CoreWebView2.Navigate(filePath);
        }
    }
}
