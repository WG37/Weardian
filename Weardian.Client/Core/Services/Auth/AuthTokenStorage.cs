using Weardian.Client.Core.Interfaces.Auth;

namespace Weardian.Client.Core.Services.Auth
{
    public class AuthTokenStorage : IAuthTokenStorage
    {
        private string? _accessToken;
        private string? _refreshToken;
        public string? GetAccessToken()
        {
            return _accessToken;
        }

        public void SetAccessToken(string accessToken)
        {
            _accessToken = accessToken;
        }

        public void ClearAccessToken()
        {
            _accessToken = null;
        }

        public string? GetRefreshToken()
        {
            return _refreshToken;
        }

        public void SetRefreshToken(string refreshToken)
        {
            _refreshToken = refreshToken;
        }

        public void ClearRefreshToken()
        {
            _refreshToken = null;
        }
    }
}
