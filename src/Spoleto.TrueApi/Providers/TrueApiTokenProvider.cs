using CIS.Cryptography;
using Spoleto.Common;
using Spoleto.Common.Helpers;

namespace Spoleto.TrueApi
{
    public class TrueApiTokenProvider : ITrueApiTokenProvider
    {
        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;

        public TrueApiTokenProvider() : this(new HttpClient(), true)
        {
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

        public async Task<TokenModel> GetTokenAsync(TrueApiProviderOption settings)
        {
            var client = _httpClient;
            client.ConfigureHttpClient();

            var authKey = await GetAuthKey(client, settings).ConfigureAwait(false);

            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, "auth/simpleSignIn"));
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                requestMessage.ConfigureRequestMessage();

                var data = Convert.ToBase64String(DefaultSettings.Encoding.GetBytes(authKey.Data));
                authKey.Data = CryptographyHelper.SignBase64Data(data, thumbprint: settings.CertificateThumbprint);

                var authKeyJson = JsonHelper.ToJson(authKey);
                requestMessage.Content = new StringContent(authKeyJson, DefaultSettings.Encoding, DefaultSettings.ContentType);
                using (var responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false))
                {
                    var result = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        TokenModel tokenModel = null;
                        try
                        {
                            tokenModel = JsonHelper.FromJson<TokenModel>(result);
                        }
                        catch (Exception e)
                        {
                            throw new Exception($"Ошибка формирования токена доступа\n{result}");
                        }

                        return tokenModel;
                    }
                    else if (!string.IsNullOrEmpty(result))
                    {
                        ErrorModel errorModel = null;
                        try
                        {
                            errorModel = JsonHelper.FromJson<ErrorModel>(result);
                        }
                        catch (Exception e)
                        {
                            errorModel = new ErrorModel { ErrorMessage = result };
                        }

                        throw new Exception(errorModel.ErrorMessage);
                    }
                    else
                    {
                        throw new Exception(responseMessage.ReasonPhrase);
                    }
                }
            }
        }

        private async Task<AuthKeyModel> GetAuthKey(HttpClient client, TrueApiProviderOption settings)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, "auth/key"));
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                requestMessage.ConfigureRequestMessage();

                using (var responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false))
                {
                    var result = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        AuthKeyModel tokenModel = null;
                        try
                        {
                            tokenModel = JsonHelper.FromJson<AuthKeyModel>(result);
                        }
                        catch (Exception e)
                        {
                            throw new Exception($"Ошибка формирования токена доступа\n{result}");
                        }

                        return tokenModel;
                    }
                    else if (!string.IsNullOrEmpty(result))
                    {
                        throw new Exception(result);
                    }
                    else
                    {

                        throw new Exception(responseMessage.ReasonPhrase);
                    }
                }
            }
        }
    }
}
