using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.HttpStatusCode;
using Weardian.Client.Core.Interfaces.Auth;
using System.Net.Http.Headers;

namespace Weardian.Client.Core.Services.Auth
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly IAuthTokenStorage _authTokenStorage;
        private readonly IAuthService _authService;

        public AuthHandler(
            IAuthTokenStorage authTokenStorage,
            IAuthService authService)
        {
            _authTokenStorage = authTokenStorage;
            _authService = authService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            var token = _authTokenStorage.GetAccessToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await base.SendAsync(request, ct);

            if (response.StatusCode == Unauthorized)
            {
                var refreshResponse = await _authService.RefreshUserToken();

                if (!refreshResponse.IsSuccessful)
                {
                    return response;
                }

                var newToken = _authTokenStorage.GetAccessToken();

                using var retryRequest = new HttpRequestMessage(
                    request.Method,
                    request.RequestUri);

                foreach (var header in request.Headers)
                {
                    retryRequest.Headers.TryAddWithoutValidation(
                        header.Key, 
                        header.Value); 
                }

                retryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);

                if (request.Content != null)
                {
                    var body = await request.Content.ReadAsByteArrayAsync(ct);

                    retryRequest.Content = new ByteArrayContent(body);

                    foreach (var header in request.Content.Headers)
                    {
                        retryRequest.Content.Headers.TryAddWithoutValidation(
                            header.Key,
                            header.Value);
                    }
                }

                response.Dispose();
                return await base.SendAsync(retryRequest, ct);

            }
            return response;
        }
    }
}
