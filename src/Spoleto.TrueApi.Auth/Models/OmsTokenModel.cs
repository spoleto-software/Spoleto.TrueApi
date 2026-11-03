using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Auth.Models
{
    /// <summary>
    /// Ответ на запрос токена СУЗ.
    /// </summary>
    public record OmsTokenModel
    {
        /// <summary>
        /// Токен аутентификации в СУЗ. Действует 10 часов.
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
