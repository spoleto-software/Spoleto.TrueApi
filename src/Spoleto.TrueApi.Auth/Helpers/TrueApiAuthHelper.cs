using Spoleto.Common;
using Spoleto.Common.Helpers;
using Spoleto.RestClient;
using Spoleto.TrueApi.Auth.Clients;
using Spoleto.TrueApi.Auth.Models;

namespace Spoleto.TrueApi.Auth.Helpers
{
    /// <summary>
    /// Общее у токенов True API и СУЗ: ключ «uuid - data», данные для подписи и разбор ответа.
    /// </summary>
    internal static class TrueApiAuthHelper
    {
        /// <summary>
        /// Пара «uuid - data» для подписи (раздел 1.5.1 True API).
        /// </summary>
        public static Task<AuthKeyModel> GetAuthKeyAsync(TrueApiRestClient restClient, string serviceUrl)
        {
            var uri = new Uri(UriHelper.UrlCombine(serviceUrl, "/api/v3/true-api/auth/key"));

            return InvokeAsync<AuthKeyModel>(restClient, TrueApiRestClient.CreateRequest(RestHttpMethod.Get, uri));
        }

        /// <summary>
        /// Данные ключа в base64: их подписывают присоединённой подписью.
        /// </summary>
        public static string GetDataToSign(AuthKeyModel authKey) => Convert.ToBase64String(DefaultSettings.Encoding.GetBytes(authKey.Data));

        public static StringContent CreateJsonContent<T>(T model) => new(JsonHelper.ToJson(model), DefaultSettings.Encoding, DefaultSettings.ContentType);

        public static async Task<T> InvokeAsync<T>(TrueApiRestClient restClient, RestRequest request)
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
    }
}
