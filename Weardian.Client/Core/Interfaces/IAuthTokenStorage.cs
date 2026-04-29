namespace Weardian.Client.Core.Interfaces
{
    public interface IAuthTokenStorage
    {
        public Task<string?> GetAccessTokenAsync();
        public Task SetAccessTokenAsync(string accessToken);
        public Task ClearAccessTokenAsync();
    }
}
