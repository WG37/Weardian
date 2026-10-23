namespace Weardian.Client.Core.Interfaces.Auth
{
    public interface IAuthTokenStorage
    {
        public string? GetAccessToken();
        public void SetAccessToken(string accessToken);
        public void ClearAccessToken();

        public string? GetRefreshToken();
        public void SetRefreshToken(string refreshToken);
        public void ClearRefreshToken();
    }
}
