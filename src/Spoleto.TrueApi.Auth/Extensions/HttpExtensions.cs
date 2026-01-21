using System.Net.Http.Headers;
using Spoleto.Common;

namespace Spoleto.TrueApi.Auth.Extensions
{
    internal static class HttpExtensions
    {
        public static void ConfigureRequestMessage(this HttpRequestMessage requestMessage)
        {
            requestMessage.Headers.Accept.Clear();
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(DefaultSettings.ContentType));
            requestMessage.Headers.AcceptCharset.ParseAdd(DefaultSettings.Charset);
        }

        public static void ConfigureHttpClient(this HttpClient client)
        {
            client.Timeout = new TimeSpan(0, 0, 5, 0);
        }
    }
}
