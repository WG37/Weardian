using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Weardian.Client.Core.Interfaces;
using Weardian.Client.Core.Interfaces.Cryptography;
using Weardian.Client.Core.Interfaces.Cryptography.Encryption;
using Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping;
using Weardian.Client.Core.Services;
using Weardian.Client.Infrastructure.Cryptography;
using Weardian.Client.Infrastructure.Cryptography.Encryption;
using Weardian.Client.Infrastructure.Cryptography.KeyWrapping;
using Weardian.Client.Infrastructure.Native.Bootstrapper;
using Weardian.Client.Infrastructure.Repositories;

namespace Weardian.Client
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

            services.AddScoped<ISymmetricKeyRepository, SymmetricKeyRepository>();
            services.AddScoped<ISymmetricKeyManagementService, SymmetricKeyManagementService>();
            services.AddScoped<IPayloadService, PayloadService>();

            services.AddScoped<ISymmetricCryptoService, SymmetricCryptoService>();
            services.AddScoped<IKeyGeneration, KeyGeneration>();
            services.AddScoped<IAesEncryptor, AesEncryptor>();

            services.AddScoped<IKeyWrappingService, KeyWrappingService>();
            services.AddScoped<IKekProvider, KekProvider>();
        }
    }

}
