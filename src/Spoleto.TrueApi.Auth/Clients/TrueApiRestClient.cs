using System.Text.Json;
using Spoleto.Common;
using Spoleto.Common.Helpers;
using Spoleto.RestClient;
using Spoleto.RestClient.Authentication;
using Spoleto.TrueApi.Auth.Models;

namespace Spoleto.TrueApi.Auth.Clients
{
    /// <summary>
    /// REST-клиент методов True API поверх переданного <see cref="HttpClient"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Неуспешный ответ, в том числе 404, - <see cref="HttpRequestException"/> с кодом ответа. Текст - из JSON ошибки сервиса
    /// (error_message с description после двоеточия, errors или description), иначе тело ответа, а без тела - ReasonPhrase.
    /// </para>
    /// <para>
    /// Клиент не освобождает <see cref="HttpClient"/> и создаётся на вызов: настройки, а с ними и токен, у методов True API свои в каждом вызове.
    /// </para>
    /// </remarks>
    public class TrueApiRestClient : RestHttpClient
    {
        /// <summary>
        /// Любой тип ответа: методы, отдающие файл, при ошибке возвращают JSON.
        /// </summary>
        private const string AnyMediaType = "*/*";

        private static readonly RestClientOptions TrueApiOptions = new() { ThrowExceptionIfNotFound = true };

        /// <param name="httpClient">Клиент HTTP, клиент True API его не освобождает.</param>
        /// <param name="authenticator">Токен для запросов, например, <see cref="TrueApiAuthenticator"/>. Без него запросы уходят без токена.</param>
        public TrueApiRestClient(HttpClient httpClient, IAuthenticator? authenticator = null)
            : base(httpClient, authenticator, TrueApiOptions, false)
        {
        }

        /// <summary>
        /// Запрос к True API: ответ в JSON, а для файла - любой, в кодировке UTF-8.
        /// </summary>
        /// <param name="method">Метод HTTP.</param>
        /// <param name="uri">Полный адрес метода.</param>
        /// <param name="content">Тело запроса.</param>
        /// <param name="isFileResponse">Метод отдаёт файл.</param>
        public static RestRequest CreateRequest(RestHttpMethod method, Uri uri, HttpContent? content = null, bool isFileResponse = false)
        {
            // AbsoluteUri, а не ToString(): ToString() раскрывает экранированные символы, и значения в строке запроса испортились бы.
            var request = new RestRequest(method, uri.AbsoluteUri, content);
            request.Headers["Accept"] = isFileResponse ? AnyMediaType : DefaultSettings.ContentType;
            request.Headers["Accept-Charset"] = DefaultSettings.Charset;

            return request;
        }

        /// <inheritdoc />
        protected override Exception CreateException(IRestResponse restResponse, string? content)
        {
            var message = content;
            if (string.IsNullOrEmpty(content))
            {
                message = restResponse.ReasonPhrase;
            }
            else if (restResponse.ContentType == Common.ContentTypes.ApplicationJson)
            {
                var errorMessage = ReadErrorMessage(content);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    message = errorMessage;
                }
            }

            return new HttpRequestException(message, null, restResponse.StatusCode);
        }

        private static string? ReadErrorMessage(string json)
        {
            try
            {
                return JsonHelper.FromJson<TrueApiErrorModel>(json)?.ToString();
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}
