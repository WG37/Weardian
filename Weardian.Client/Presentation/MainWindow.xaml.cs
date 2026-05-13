using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System.Windows;
using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Core.Interfaces.Symmetric;

namespace Weardian.Client.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IKeyManagementService _symmetricManagementService;
        private readonly IPayloadService _payloadService;

        public MainWindow(
            IKeyManagementService symmetricManagementService,
            IPayloadService payloadService)
        {
            InitializeComponent();
            InitializeBrowser();

            _symmetricManagementService = symmetricManagementService;
            _payloadService = payloadService;
        }

        private async void InitializeBrowser()
        {
            await Browser.EnsureCoreWebView2Async();

            Browser.CoreWebView2.WebMessageReceived += (sender, args) =>
            {
                var message = args.WebMessageAsJson;

                MessageBox.Show(message);
            };

            Browser.CoreWebView2.Navigate("http://localhost:5173");
        }

    }
}