using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
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

            services.AddScoped<IPayloadRecordRepository, PayloadRecordRepository>();
            services.AddScoped<IKeyRecordRepository, KeyRecordRepository>();
            services.AddScoped<IKeyManagementService, KeyManagementService>();
            services.AddScoped<IPayloadService, PayloadService>();

            services.AddScoped<ISymmetricCryptoService, SymmetricCryptoService>();
            services.AddScoped<IKeyGeneration, KeyGeneration>();
            services.AddScoped<IAesEncryptor, AesEncryptor>();

            services.AddScoped<IKeyWrappingService, KeyWrappingService>();
            services.AddScoped<IKekProvider, KekProvider>();

            services.AddScoped<IAuthTokenStorage, AuthTokenStorage>();

            services.AddScoped<IInputValidationService, InputValidationService>();

            services.AddHttpClient<IKeyRecordSyncService, KeyRecordSyncService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7259");
            });

            services.AddScoped<MainWindow>();

            var serviceProvider = services.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
