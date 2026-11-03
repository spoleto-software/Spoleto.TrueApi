using CIS.Cryptography;
using Spoleto.RestClient;
using Spoleto.TrueApi.Auth.Clients;
using Spoleto.TrueApi.Auth.Extensions;
using Spoleto.TrueApi.Auth.Helpers;
using Spoleto.TrueApi.Auth.Models;
using Spoleto.TrueApi.Auth.Models.Options;

namespace Spoleto.TrueApi.Auth.Providers
{
    /// <summary>
    /// Токен аутентификации в СУЗ через True API (раздел 11.1): ключ - «/auth/key», токен - «/auth/simpleSignIn/{omsConnection}».
    /// </summary>
    /// <remarks>
    /// <para>
    /// Это токен СУЗ, а не токен методов True API, поэтому провайдер отдельный от <see cref="TrueApiTokenProvider"/>.
    /// </para>
    /// <para>
    /// Токен хранится, пока не истёк, и только для настроек, с которыми получен: с другими настройками запрашивается новый.
    /// Каждый новый токен - это подпись в КриптоПро.
    /// </para>
    /// </remarks>
    public class TrueApiOmsTokenProvider : ITrueApiOmsTokenProvider, IDisposable
    {
        /// <summary>
        /// Срок действия токена по описанию метода.
        /// </summary>
        public static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(10);

        /// <summary>
        /// Токен обновляется раньше срока: запрос в СУЗ не должен уйти с токеном на исходе.
        /// </summary>
        private static readonly TimeSpan RefreshMargin = TimeSpan.FromMinutes(10);

        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;
        private readonly TimeProvider _timeProvider;

        private OmsTokenModel? _token;
        private TrueApiOmsTokenProviderOption? _tokenSettings;
        private DateTimeOffset _tokenRefreshTime;

        public TrueApiOmsTokenProvider() : this(new HttpClient(), true)
        {
            _httpClient.ConfigureHttpClient();
        }

        public TrueApiOmsTokenProvider(HttpClient httpClient, bool disposeHttpClient) : this(httpClient, disposeHttpClient, TimeProvider.System)
        {
        }

        /// <param name="timeProvider">Часы для срока действия токена.</param>
        public TrueApiOmsTokenProvider(HttpClient httpClient, bool disposeHttpClient, TimeProvider timeProvider)
        {
            _httpClient = httpClient;
            _disposeHttpClient = disposeHttpClient;
            _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
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

        public async Task<OmsTokenModel> GetTokenAsync(TrueApiOmsTokenProviderOption settings)
        {
            ArgumentNullException.ThrowIfNull(settings);
            ArgumentException.ThrowIfNullOrEmpty(settings.ServiceUrl, nameof(settings.ServiceUrl));
            ArgumentException.ThrowIfNullOrEmpty(settings.OmsConnection, nameof(settings.OmsConnection));

            if (_token != null && settings == _tokenSettings && _timeProvider.GetUtcNow() < _tokenRefreshTime)
            {
                return _token;
            }

            // Срок считается от начала запроса: так токен заведомо не переживёт свой срок.
            var requestTime = _timeProvider.GetUtcNow();

            // Запросы самого токена идут без токена: у клиента нет аутентификатора.
            using var restClient = new TrueApiRestClient(_httpClient);

            var authKey = await TrueApiAuthHelper.GetAuthKeyAsync(restClient, settings.ServiceUrl).ConfigureAwait(false);

            var requestModel = new OmsTokenRequest
            {
                Uuid = authKey.Uuid,
                Data = SignAttached(TrueApiAuthHelper.GetDataToSign(authKey), settings.CertificateThumbprint),
                Inn = string.IsNullOrEmpty(settings.Inn) ? null : settings.Inn
            };

            var signInUri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/api/v3/true-api/auth/simpleSignIn/{Uri.EscapeDataString(settings.OmsConnection)}"));
            var signInRequest = TrueApiRestClient.CreateRequest(RestHttpMethod.Post, signInUri, TrueApiAuthHelper.CreateJsonContent(requestModel));
            var token = await TrueApiAuthHelper.InvokeAsync<OmsTokenModel>(restClient, signInRequest).ConfigureAwait(false);
            if (string.IsNullOrEmpty(token?.Token))
            {
                throw new InvalidDataException($"True API не вернул токен СУЗ для соединения {settings.OmsConnection}.");
            }

            _token = token;
            _tokenSettings = settings;
            _tokenRefreshTime = requestTime + TokenLifetime - RefreshMargin;

            return _token;
        }

        public void SetTokenExpired()
        {
            _token = null;
            _tokenSettings = null;
        }

        /// <summary>
        /// Присоединённая подпись данных ключа сертификатом из настроек.
        /// </summary>
        /// <param name="base64Data">Данные ключа в base64.</param>
        /// <param name="certificateThumbprint">Отпечаток сертификата.</param>
        protected virtual string SignAttached(string base64Data, string certificateThumbprint)
            => CryptographyHelper.SignBase64Data(base64Data, thumbprint: certificateThumbprint);
    }
}
