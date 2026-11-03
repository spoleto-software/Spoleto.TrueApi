using CIS.Cryptography;
using Spoleto.Common;
using Spoleto.Common.Helpers;
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

        private UnitedTokenModel? _token;

        public TrueApiTokenProvider() : this(new HttpClient(), true)
        {
            _httpClient.ConfigureHttpClient();
        }

        public TrueApiTokenProvider(HttpClient httpClient, bool disposeHttpClient)
        {
            _httpClient = httpClient;
            _disposeHttpClient = disposeHttpClient;
        }

        #region IDisposable
        bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;
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

            // Запросы самого токена идут без токена: у клиента нет аутентификатора.
            using var restClient = new TrueApiRestClient(_httpClient);

            var keyUri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, "/api/v3/true-api/auth/key"));
            var authKey = await InvokeAsync<AuthKeyModel>(restClient, TrueApiRestClient.CreateRequest(RestHttpMethod.Get, keyUri)).ConfigureAwait(false);

            var data = Convert.ToBase64String(DefaultSettings.Encoding.GetBytes(authKey.Data));
            var requestModel = new TokenRequest
            {
                Uuid = authKey.Uuid,
                Data = CryptographyHelper.SignBase64Data(data, thumbprint: settings.CertificateThumbprint),
                Inn = settings.Inn,
                UnitedToken = settings.UnitedToken
            };

            var requestContent = new StringContent(JsonHelper.ToJson(requestModel), DefaultSettings.Encoding, DefaultSettings.ContentType);
            var signInUri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, "/api/v3/true-api/auth/simpleSignIn"));
            _token = await InvokeAsync<UnitedTokenModel>(restClient, TrueApiRestClient.CreateRequest(RestHttpMethod.Post, signInUri, requestContent)).ConfigureAwait(false);

            return _token;
        }

        private static async Task<T> InvokeAsync<T>(TrueApiRestClient restClient, RestRequest request)
        {
            var response = await restClient.ExecuteAsStringAsync(request).ConfigureAwait(false);
            try
            {
                return JsonHelper.FromJson<T>(response.Content);
            }
            catch (Exception e)
            {
                throw new Exception($"Ошибка формирования токена доступа\n{response.Content}", e);
            }
        }

        public void SetTokenExpired()
        {
            _token = null;
        }
    }
}
