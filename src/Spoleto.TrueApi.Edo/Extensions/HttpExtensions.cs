using System.Net.Http.Headers;
using Spoleto.Common;

namespace Spoleto.TrueApi.Edo.Extensions
{
    internal static class HttpExtensions
    {
        /// <summary>
        /// Любой тип ответа: методы, отдающие файл, при ошибке возвращают JSON.
        /// </summary>
        private const string AnyMediaType = "*/*";

        public static void ConfigureRequestMessage(this HttpRequestMessage requestMessage, bool isFileResponse = false)
        {
            requestMessage.Headers.Accept.Clear();
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(isFileResponse ? AnyMediaType : DefaultSettings.ContentType));
            requestMessage.Headers.AcceptCharset.ParseAdd(DefaultSettings.Charset);
        }

        public static void ConfigureHttpClient(this HttpClient client)
        {
            client.Timeout = new TimeSpan(0, 0, 5, 0);
        }
    }
}
