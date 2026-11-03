using CIS.Cryptography;
using Spoleto.RestClient;
using Spoleto.TrueApi.Auth.Clients;
using Spoleto.TrueApi.Auth.Extensions;
using Spoleto.TrueApi.Auth.Helpers;
using Spoleto.TrueApi.Auth.Models;
using Spoleto.TrueApi.Auth.Models.Options;

namespace Spoleto.TrueApi.Auth.Providers
{
    public class TrueApiTokenProvider : ITrueApiTokenProvider
    {
        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;

        // Запросы самого токена идут без токена: у клиента нет аутентификатора.
        private readonly TrueApiRestClient _restClient;

        private UnitedTokenModel? _token;

        public TrueApiTokenProvider() : this(new HttpClient(), true)
        {
            _httpClient.ConfigureHttpClient();
        }

        public TrueApiTokenProvider(HttpClient httpClient, bool disposeHttpClient)
        {
            _httpClient = httpClient;
            _disposeHttpClient = disposeHttpClient;
            _restClient = new TrueApiRestClient(httpClient);
        }

        #region IDisposable
        bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;
                _restClient.Dispose();
                if (_disposeHttpClient)
                {
                    _httpClient.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public async Task<UnitedTokenModel> GetTokenAsync(TrueApiTokenProviderOption settings)
        {
            if (_token != null)
            {
                return _token;
            }

            var authKey = await TrueApiAuthHelper.GetAuthKeyAsync(_restClient, settings.ServiceUrl).ConfigureAwait(false);

            var requestModel = new TokenRequest
            {
                Uuid = authKey.Uuid,
                Data = CryptographyHelper.SignBase64Data(TrueApiAuthHelper.GetDataToSign(authKey), thumbprint: settings.CertificateThumbprint),
                Inn = settings.Inn,
                UnitedToken = settings.UnitedToken
            };

            var signInUri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, "/api/v3/true-api/auth/simpleSignIn"));
            var signInRequest = TrueApiRestClient.CreateRequest(RestHttpMethod.Post, signInUri, TrueApiAuthHelper.CreateJsonContent(requestModel));
            _token = await TrueApiAuthHelper.InvokeAsync<UnitedTokenModel>(_restClient, signInRequest).ConfigureAwait(false);

            return _token;
        }

        public void SetTokenExpired()
        {
            _token = null;
        }
    }
}
