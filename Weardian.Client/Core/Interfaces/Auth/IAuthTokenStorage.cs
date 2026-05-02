namespace Weardian.Client.Core.Interfaces.Auth
{
    public interface IAuthTokenStorage
    {
        public Task<string?> GetAccessTokenAsync();
        public Task SetAccessTokenAsync(string accessToken);
        public Task ClearAccessTokenAsync();
    }
}
