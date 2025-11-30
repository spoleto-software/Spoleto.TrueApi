using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    ///  Пакет успешного ответа на запрос получения предварительного ключа перед запросом токена
    /// </summary>
    public class AuthKeyModel
    {
        /// <summary>
        /// Уникальный идентификатор сгенерированных случайных данных.
        /// </summary>
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Случайная строка данных.
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}
