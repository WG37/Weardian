using System.Windows;
using Weardian.Client.Core.Interfaces.Handler;

namespace Weardian.Client.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMessageHandlerService _symmetricHandlerService;

        public MainWindow(
            IMessageHandlerService symmetricHandlerService)
        {
            _symmetricHandlerService = symmetricHandlerService;

            InitializeComponent();
            InitializeBrowser();
        }

        private async void InitializeBrowser()
        {
            await ClientWebView.EnsureCoreWebView2Async();

            ClientWebView.CoreWebView2.WebMessageReceived += async (sender, args) =>
            {
                var request = args.WebMessageAsJson;

                var response = await _symmetricHandlerService.HandleAsync(request);

                ClientWebView.CoreWebView2.PostWebMessageAsJson(response);
            };

            ClientWebView.CoreWebView2.Navigate("http://localhost:5173");
        }

    }
}