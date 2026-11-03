using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Auth.Models
{
    /// <summary>
    /// Тело запроса токена СУЗ: без «unitedToken» и «details» запроса токена True API.
    /// </summary>
    internal record OmsTokenRequest
    {
        /// <summary>
        /// Уникальный идентификатор сгенерированных случайных данных.
        /// </summary>
        [JsonPropertyName("uuid")]
        public string? Uuid { get; set; }

        /// <summary>
        /// Случайные данные в base64, подписанные УКЭП участника оборота товаров (присоединённая электронная подпись).
        /// </summary>
        [JsonPropertyName("data")]
        public string? Data { get; set; }

        /// <summary>
        /// ИНН организации, под которой требуется авторизация по МЧД.
        /// </summary>
        /// <remarks>
        /// Без значения не передаётся: пустой ИНН сервис отклоняет как ИНН не из 10 или 12 цифр.
        /// </remarks>
        [JsonPropertyName("inn")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Inn { get; set; }
    }
}
