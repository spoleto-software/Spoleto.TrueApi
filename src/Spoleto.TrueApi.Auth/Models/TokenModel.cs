using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Auth.Models
{
    /// <summary>
    ///  Пакет успешного ответа на POST запрос получения токена.
    /// </summary>
    [Obsolete]
    public record TokenModel
    {
        /// <summary>
        /// Токен.
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; }
   }
}