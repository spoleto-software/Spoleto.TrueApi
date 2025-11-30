using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    ///  Пакет успешного ответа на POST запрос получения токена.
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// Токен.
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }

}