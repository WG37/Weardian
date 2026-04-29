using Weardian.Client.Core.Interfaces;

namespace Weardian.Client.Core.Services
{
    public class AuthTokenStorage : IAuthTokenStorage
    {
        private string? _accessToken;

        public Task<string?> GetAccessTokenAsync()
        {
            return Task.FromResult(_accessToken);
        }

        public Task SetAccessTokenAsync(string accessToken)
        {
            _accessToken = accessToken;

            return Task.CompletedTask;
        }

        public Task ClearAccessTokenAsync()
        {
            _accessToken = null;

            return Task.CompletedTask;
        }
    }
}
