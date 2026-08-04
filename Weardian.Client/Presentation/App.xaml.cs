using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Weardian.Client.Core.Interfaces.Auth;
using Weardian.Client.Core.Interfaces.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography.Encryption;
using Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping;
using Weardian.Client.Core.Interfaces.InputValidation;
using Weardian.Client.Core.Interfaces.Symmetric;
using Weardian.Client.Core.Interfaces.Symmetric.Repositories;
using Weardian.Client.Core.Interfaces.Sync;
using Weardian.Client.Core.Services.Auth;
using Weardian.Client.Core.Services.InputValidation;
using Weardian.Client.Core.Services.Symmetric;
using Weardian.Client.Core.Services.Sync;
using Weardian.Client.Infrastructure.Cryptography;
using Weardian.Client.Infrastructure.Cryptography.Encryption;
using Weardian.Client.Infrastructure.Cryptography.KeyWrapping;
using Weardian.Client.Infrastructure.Native.Bootstrapper;
using Weardian.Client.Infrastructure.Repositories.Symmetric;

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

            services.AddScoped<IPayloadRecordRepository, PayloadRecordRepository>();
            services.AddScoped<IKeyRecordRepository, KeyRecordRepository>();

            services.AddScoped<IKeyManagementService, KeyManagementService>();

            services.AddScoped<IPayloadRecordSyncService, PayloadRecordSyncService>();
            services.AddScoped<IKeyRecordSyncService, KeyRecordSyncService>();

            services.AddScoped<IPayloadService, PayloadService>();

            services.AddScoped<IAuthTokenStorage, AuthTokenStorage>();

            services.AddTransient<ISymmetricCryptoService, SymmetricCryptoService>();
            services.AddTransient<IKeyGeneration, KeyGeneration>();
            services.AddTransient<IAesEncryptor, AesEncryptor>();

            services.AddTransient<IKeyWrappingService, KeyWrappingService>();
            services.AddTransient<IKekProvider, KekProvider>();


            services.AddTransient<IInputValidationService, InputValidationService>();
            services.AddTransient<ISymmetricMessageHandlerService, SymmetricMessageHandlerService>();
            

            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = apiBaseUrl;
            });

            services.AddHttpClient<IEnvelopeSyncService, EnvelopeSyncService>(client =>
            {
                client.BaseAddress = apiBaseUrl;
            });

            services.AddScoped<MainWindow>();

            var serviceProvider = services.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
