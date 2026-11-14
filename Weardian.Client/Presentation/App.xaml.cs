using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Weardian.Client.Core.Interfaces.APIClient.Sync;
using Weardian.Client.Core.Interfaces.Auth;
using Weardian.Client.Core.Interfaces.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography.Encryption;
using Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping;
using Weardian.Client.Core.Interfaces.Handler;
using Weardian.Client.Core.Interfaces.InputValidation;
using Weardian.Client.Core.Interfaces.Symmetric;
using Weardian.Client.Core.Interfaces.Symmetric.Repositories;
using Weardian.Client.Core.Interfaces.Sync;
using Weardian.Client.Core.Services.Handler;
using Weardian.Client.Core.Services.InputValidation;
using Weardian.Client.Core.Services.Symmetric;
using Weardian.Client.Core.Services.Sync;
using Weardian.Client.Infrastructure.Cryptography;
using Weardian.Client.Infrastructure.Cryptography.Encryption;
using Weardian.Client.Infrastructure.Cryptography.KeyWrapping;
using Weardian.Client.Infrastructure.Native.Bootstrapper;
using Weardian.Client.Infrastructure.Repositories.Symmetric;
using Weardian.Client.Infrastructure.Services.APIClient.Sync;
using Weardian.Client.Infrastructure.Services.Auth;
using Weardian.Client.Infrastructure.Services.Shared;
using Weardian.Client.Infrastructure.Services.Sync;

namespace Weardian.Client.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            await VaultBootstrap.EnsureInitializedAsync();

            var services = new ServiceCollection();

            var apiBaseUrl = new Uri("https://localhost:7259");

            // repos
            services.AddScoped<IPayloadRecordRepository, PayloadRecordRepository>();
            services.AddScoped<IKeyRecordRepository, KeyRecordRepository>();

            // main orchestrators
            services.AddScoped<IKeyManagementService, KeyManagementService>();
            services.AddTransient<IMessageHandlerService, MessageHandlerService>();

            // synchronization
            services.AddScoped<IEnvelopeTransferService, EnvelopeTransferService>();
            services.AddScoped<IPayloadRecordSyncService, PayloadRecordSyncService>();
            services.AddScoped<IKeyRecordSyncService, KeyRecordSyncService>();
            
            // local
            services.AddScoped<IPayloadService, PayloadService>();

            // cryptography
            services.AddTransient<ISymmetricCryptoService, SymmetricCryptoService>();
            services.AddTransient<IKeyGeneration, KeyGeneration>();
            services.AddTransient<IAesEncryptor, AesEncryptor>();
            services.AddTransient<IKeyWrappingService, KeyWrappingService>();
            services.AddTransient<IKekProvider, KekProvider>();

            // validation
            services.AddTransient<IInputValidationService, InputValidationService>();

            // authentication
            services.AddSingleton<IAuthTokenStorage, AuthTokenStorage>();
            services.AddTransient<AuthHandler>();

            services.AddSingleton<SharedSemaphore>();

            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = apiBaseUrl;
            });

            // envelopes API client
            services.AddHttpClient<IEnvelopeAPIClient, EnvelopeAPIClient>(client =>
            {
                client.BaseAddress = apiBaseUrl;
            }).AddHttpMessageHandler<AuthHandler>();

            services.AddScoped<MainWindow>();

            var serviceProvider = services.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
