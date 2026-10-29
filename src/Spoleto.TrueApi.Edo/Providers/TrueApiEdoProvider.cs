using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CIS.Cryptography;
using Spoleto.Common;
using Spoleto.Common.Helpers;
using Spoleto.TrueApi.Auth.Models;
using Spoleto.TrueApi.Auth.Providers;
using Spoleto.TrueApi.Edo.Exceptions;
using Spoleto.TrueApi.Edo.Extensions;
using Spoleto.TrueApi.Edo.Helpers;
using Spoleto.TrueApi.Edo.Models;
using Spoleto.TrueApi.Edo.Models.Options;
using Spoleto.TrueApi.Edo.Models.Upd;

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

        private async Task<HttpResponseMessage> SendWithTokenAsync(TrueApiEdoProviderOption settings, HttpMethod method, Uri uri, bool isFileResponse, Func<HttpContent>? createContent)
        {
            using var requestMessage = new HttpRequestMessage(method, uri);
            requestMessage.ConfigureRequestMessage(isFileResponse);
            requestMessage.Content = createContent?.Invoke();

            var token = await GetTokenAsync(settings).ConfigureAwait(false);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.UuidToken);

            return await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Отправка запроса. Возвращает только успешный ответ, иначе исключение с текстом ошибки сервиса.
        /// </summary>
        /// <param name="createContent">Тело запроса. Создаётся на каждую попытку заново: отправленный запрос освобождает своё тело.</param>
        private async Task<HttpResponseMessage> SendAsync(TrueApiEdoProviderOption settings, HttpMethod method, Uri uri, bool isFileResponse = false, Func<HttpContent>? createContent = null)
        {
            var responseMessage = await SendWithTokenAsync(settings, method, uri, isFileResponse, createContent).ConfigureAwait(false);

            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Токен истёк или аннулирован: запрашиваем новый один раз. Повторный 401 - уже не токен, отдаём ошибку.
                responseMessage.Dispose();
                _tokenProvider.SetTokenExpired();
                responseMessage = await SendWithTokenAsync(settings, method, uri, isFileResponse, createContent).ConfigureAwait(false);
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

        private async Task<T> InvokeAsync<T>(TrueApiEdoProviderOption settings, HttpMethod method, Uri uri, Func<HttpContent>? createContent = null)
        {
            using var responseMessage = await SendAsync(settings, method, uri, createContent: createContent).ConfigureAwait(false);

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

        // Подписание исходящих документов - в разделе edo, а списки и файлы - в elk.
        private const string EdoOutgoingDocumentsPath = "/api/v3/true-api/edo/outgoing-documents";

        // Загрузка УПДи - отдельный метод.
        private const string UpdiPath = "/xml/updi";

        private const string Base64ContentEncoding = "base64";

        private const string XmlMediaType = "text/xml";

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

        private async Task<EdoSignedDocumentInfo> GetSignedDocumentInfoAsync(TrueApiEdoProviderOption settings, string documentsPath, string documentId)
        {
            ArgumentException.ThrowIfNullOrEmpty(documentId);

            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"{documentsPath}/{Uri.EscapeDataString(documentId)}"));

            using var responseMessage = await SendAsync(settings, HttpMethod.Get, uri, isFileResponse: true).ConfigureAwait(false);

            var contentDisposition = responseMessage.Content.Headers.ContentDisposition;
            var archiveFileName = contentDisposition?.FileNameStar ?? contentDisposition?.FileName?.Trim('"');

            var archiveBytes = await responseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using var archive = new ZipArchive(new MemoryStream(archiveBytes), ZipArchiveMode.Read);

            return EdoArchiveHelper.ReadSignedDocument(archive, archiveFileName);
        }

        private async Task<EdoSellerDocumentInfo> GetSellerDocumentInfoAsync(TrueApiEdoProviderOption settings, string documentsPath, string documentId)
        {
            var document = await GetSignedDocumentInfoAsync(settings, documentsPath, documentId).ConfigureAwait(false);

            return EdoSellerDocumentHelper.ReadSellerDocumentInfo(document);
        }

        /// <summary>
        /// Открепленная подпись файла сертификатом из настроек.
        /// </summary>
        /// <param name="settings">Настройки провайдера: отпечаток сертификата.</param>
        /// <param name="content">Файл документа как есть, байт в байт.</param>
        /// <returns>Подпись CMS в Base64.</returns>
        protected virtual string SignDetached(TrueApiEdoProviderOption settings, byte[] content)
            => CryptographyHelper.SignBase64Data(Convert.ToBase64String(content), detached: true, thumbprint: settings.CertificateThumbprint);

        private static StringContent CreateSignatureContent(string signature)
        {
            var content = new StringContent(signature, Encoding.UTF8, ContentTypes.TextPlain);
            content.Headers.ContentEncoding.Add(Base64ContentEncoding);

            return content;
        }

        /// <summary>
        /// Тело загрузки файла: файл в поле content и, если документ не черновик, подпись в поле signature.
        /// </summary>
        private static MultipartFormDataContent CreateDocumentContent(string fileId, byte[] rawContent, string? signature)
        {
            var fileContent = new ByteArrayContent(rawContent);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(XmlMediaType);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = "\"content\"", FileName = $"\"{fileId}.xml\"" };

            var content = new MultipartFormDataContent { fileContent };
            if (signature != null)
            {
                content.Add(CreateSignatureContent(signature), "signature");
            }

            return content;
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

        /// <inheritdoc />
        public Task<EdoSignedDocumentInfo> GetIncomingSignedDocumentInfoAsync(TrueApiEdoProviderOption settings, string documentId)
            => GetSignedDocumentInfoAsync(settings, IncomingDocumentsPath, documentId);

        /// <inheritdoc />
        public Task<EdoSignedDocumentInfo> GetOutgoingSignedDocumentInfoAsync(TrueApiEdoProviderOption settings, string documentId)
            => GetSignedDocumentInfoAsync(settings, OutgoingDocumentsPath, documentId);

        /// <inheritdoc />
        public Task<EdoSellerDocumentInfo> GetIncomingSellerDocumentInfoAsync(TrueApiEdoProviderOption settings, string documentId)
            => GetSellerDocumentInfoAsync(settings, IncomingDocumentsPath, documentId);

        /// <inheritdoc />
        public Task<EdoSellerDocumentInfo> GetOutgoingSellerDocumentInfoAsync(TrueApiEdoProviderOption settings, string documentId)
            => GetSellerDocumentInfoAsync(settings, OutgoingDocumentsPath, documentId);

        /// <inheritdoc />
        public async Task SignOutgoingDocumentAsync(TrueApiEdoProviderOption settings, string documentId)
        {
            // Подписываем байты, полученные от сервиса, без перекодирования: подпись сверяется с файлом черновика.
            var content = await GetOutgoingDocumentAsync(settings, documentId).ConfigureAwait(false);
            var signature = SignDetached(settings, content);

            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"{EdoOutgoingDocumentsPath}/{Uri.EscapeDataString(documentId)}/signature"));

            using var responseMessage = await SendAsync(settings, HttpMethod.Post, uri, createContent: () => CreateSignatureContent(signature)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EdoPostDocumentResult> PostUniversalTransferDocumentAsync(TrueApiEdoProviderOption settings, EdoSellerUniversalTransferDocument document, bool isDraft = false)
        {
            ArgumentNullException.ThrowIfNull(document);

            var fileId = string.IsNullOrEmpty(document.FileId) ? EdoUniversalTransferDocumentXmlHelper.GetFileId(document) : document.FileId;
            var content = EdoUniversalTransferDocumentXmlHelper.CreateXml(document, fileId);
            var rawContent = EdoUniversalTransferDocumentXmlHelper.FileEncoding.GetBytes(content);

            // Без подписи документ остаётся черновиком, с подписью сразу уходит получателю.
            var signature = isDraft ? null : SignDetached(settings, rawContent);

            var path = document.IsUPDi ? EdoOutgoingDocumentsPath + UpdiPath : EdoOutgoingDocumentsPath;
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, path));

            var result = await InvokeAsync<EdoIdResult>(settings, HttpMethod.Post, uri, () => CreateDocumentContent(fileId, rawContent, signature)).ConfigureAwait(false);
            if (string.IsNullOrEmpty(result?.Id))
            {
                throw new InvalidDataException($"«ЭДО Лайт» не вернул идентификатор загруженного файла {fileId}.");
            }

            return new EdoPostDocumentResult
            {
                Id = result.Id,
                FileId = fileId,
                Content = content,
                RawContent = rawContent
            };
        }
    }
}
