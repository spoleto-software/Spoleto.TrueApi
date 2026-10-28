using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Spoleto.Common;
using Spoleto.Common.Helpers;
using Spoleto.TrueApi.Auth.Models;
using Spoleto.TrueApi.Auth.Providers;
using Spoleto.TrueApi.Edo.Exceptions;
using Spoleto.TrueApi.Edo.Extensions;
using Spoleto.TrueApi.Edo.Helpers;
using Spoleto.TrueApi.Edo.Models;
using Spoleto.TrueApi.Edo.Models.Options;

namespace Spoleto.TrueApi.Edo.Providers
{
    /// <summary>
    /// Провайдер методов «ЭДО Лайт» в True API.
    /// </summary>
    public class TrueApiEdoProvider : ITrueApiEdoProvider, IDisposable
    {
        private readonly ITrueApiTokenProvider _tokenProvider;

        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;

        public TrueApiEdoProvider() : this(new HttpClient(), true)
        {
            _httpClient.ConfigureHttpClient();
        }

        public TrueApiEdoProvider(HttpClient httpClient, bool disposeHttpClient) : this(httpClient, disposeHttpClient, new TrueApiTokenProvider(httpClient, false))
        {
        }

        public TrueApiEdoProvider(HttpClient httpClient, bool disposeHttpClient, ITrueApiTokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _disposeHttpClient = disposeHttpClient;
            _tokenProvider = tokenProvider;
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

        private Task<UnitedTokenModel> GetTokenAsync(TrueApiEdoProviderOption settings) => _tokenProvider.GetTokenAsync(new(settings.ServiceUrl, settings.CertificateThumbprint, settings.Inn, true));

        private async Task<HttpResponseMessage> SendWithTokenAsync(TrueApiEdoProviderOption settings, HttpMethod method, Uri uri, bool isFileResponse)
        {
            using var requestMessage = new HttpRequestMessage(method, uri);
            requestMessage.ConfigureRequestMessage(isFileResponse);

            var token = await GetTokenAsync(settings).ConfigureAwait(false);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.UuidToken);

            return await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Отправка запроса. Возвращает только успешный ответ, иначе исключение с текстом ошибки сервиса.
        /// </summary>
        private async Task<HttpResponseMessage> SendAsync(TrueApiEdoProviderOption settings, HttpMethod method, Uri uri, bool isFileResponse = false)
        {
            var responseMessage = await SendWithTokenAsync(settings, method, uri, isFileResponse).ConfigureAwait(false);

            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Токен истёк или аннулирован: запрашиваем новый один раз. Повторный 401 - уже не токен, отдаём ошибку.
                responseMessage.Dispose();
                _tokenProvider.SetTokenExpired();
                responseMessage = await SendWithTokenAsync(settings, method, uri, isFileResponse).ConfigureAwait(false);
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                return responseMessage;
            }

            using (responseMessage)
            {
                throw await CreateExceptionAsync(responseMessage).ConfigureAwait(false);
            }
        }

        private static async Task<HttpRequestException> CreateExceptionAsync(HttpResponseMessage responseMessage)
        {
            var errorResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var message = errorResult;
            if (string.IsNullOrEmpty(errorResult))
            {
                message = responseMessage.ReasonPhrase;
            }
            else if (responseMessage.Content.Headers.ContentType?.MediaType == ContentTypes.ApplicationJson)
            {
                try
                {
                    var errorModel = JsonHelper.FromJson<EdoErrorModel>(errorResult);
                    var errorMessage = errorModel?.ToString();
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        message = errorMessage;
                    }
                }
                catch (JsonException)
                {
                }
            }

            return new HttpRequestException(message, null, responseMessage.StatusCode);
        }

        private async Task<T> InvokeAsync<T>(TrueApiEdoProviderOption settings, HttpMethod method, Uri uri)
        {
            using var responseMessage = await SendAsync(settings, method, uri).ConfigureAwait(false);

            var result = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                return JsonHelper.FromJson<T>(result)!;
            }
            catch (JsonException ex)
            {
                throw new JsonParsingException(result, ex);
            }
        }

        private async Task<byte[]> InvokeFileAsync(TrueApiEdoProviderOption settings, HttpMethod method, Uri uri)
        {
            using var responseMessage = await SendAsync(settings, method, uri, isFileResponse: true).ConfigureAwait(false);

            return await responseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        private const string IncomingDocumentsPath = "/api/v3/true-api/elk/incoming-documents";

        private const string OutgoingDocumentsPath = "/api/v3/true-api/elk/outgoing-documents";

        private async Task<EdoDocumentCollection> GetDocumentListAsync(TrueApiEdoProviderOption settings, string documentsPath, EdoDocumentSearchModel? searchModel)
        {
            var path = documentsPath;
            if (searchModel != null)
            {
                var queryString = HttpHelper.ToQueryString(searchModel);
                if (queryString.Length > 0)
                {
                    path += $"?{queryString}";
                }
            }

            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, path));

            return await InvokeAsync<EdoDocumentCollection>(settings, HttpMethod.Get, uri).ConfigureAwait(false);
        }

        private async Task<byte[]> GetDocumentAsync(TrueApiEdoProviderOption settings, string documentsPath, string documentId)
        {
            ArgumentException.ThrowIfNullOrEmpty(documentId);

            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"{documentsPath}/{Uri.EscapeDataString(documentId)}/content"));

            return await InvokeFileAsync(settings, HttpMethod.Get, uri).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<EdoDocumentCollection> GetIncomingDocumentListAsync(TrueApiEdoProviderOption settings, EdoDocumentSearchModel? searchModel = null)
            => GetDocumentListAsync(settings, IncomingDocumentsPath, searchModel);

        /// <inheritdoc />
        public Task<byte[]> GetIncomingDocumentAsync(TrueApiEdoProviderOption settings, string documentId)
            => GetDocumentAsync(settings, IncomingDocumentsPath, documentId);

        /// <inheritdoc />
        public Task<EdoDocumentCollection> GetOutgoingDocumentListAsync(TrueApiEdoProviderOption settings, EdoDocumentSearchModel? searchModel = null)
            => GetDocumentListAsync(settings, OutgoingDocumentsPath, searchModel);

        /// <inheritdoc />
        public Task<byte[]> GetOutgoingDocumentAsync(TrueApiEdoProviderOption settings, string documentId)
            => GetDocumentAsync(settings, OutgoingDocumentsPath, documentId);
    }
}
