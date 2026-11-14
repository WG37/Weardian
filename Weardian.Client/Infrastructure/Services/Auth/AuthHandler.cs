using System.Net.Http;
using System.Net.Http.Headers;
using Weardian.Client.Core.Interfaces.Auth;
using Weardian.Client.Infrastructure.Services.Shared;
using static System.Net.HttpStatusCode;

namespace Weardian.Client.Infrastructure.Services.Auth
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly IAuthTokenStorage _authTokenStorage;
        private readonly IAuthService _authService;
        private readonly SharedSemaphore _semaphore;

        public AuthHandler(
            IAuthTokenStorage authTokenStorage,
            IAuthService authService,
            SharedSemaphore semaphore)
        {
            _authTokenStorage = authTokenStorage;
            _authService = authService;
            _semaphore = semaphore;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            var token = _authTokenStorage.GetAccessToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (request.Content != null)
            {
                await request.Content.LoadIntoBufferAsync();
            }

            var response = await base.SendAsync(request, ct);

            if (response.StatusCode == Unauthorized)
            {
                await _semaphore.RefreshLock.WaitAsync(ct);
                try
                {
                    var storedToken = _authTokenStorage.GetAccessToken();
                    // if the stored token does not match the request token,
                    // another operation has likely already refreshed it, so skip refreshing
                    if (storedToken == token)
                    {
                        var refreshResponse = await _authService.RefreshUserToken();

                        if (!refreshResponse.IsSuccessful)
                            return response;
                    }
                }
                finally
                {
                    _semaphore.RefreshLock.Release();
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
                // read original request body as bytes to preserve its content regardless of format
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
