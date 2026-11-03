using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Auth.Models
{
    /// <summary>
    /// Ответ на запрос токена СУЗ.
    /// </summary>
    /// <remarks>
    /// Ошибка обычно приходит с кодом 400, но старый адрес «auth/cert» отвечал на неё кодом 200, поэтому поля ошибки - в той же модели.
    /// Поле «code» не читается: в ответах True API оно встречается и строкой, и числом.
    /// </remarks>
    public record OmsTokenModel
    {
        /// <summary>
        /// Токен аутентификации в СУЗ. Действует 10 часов.
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>
        /// Сообщение об ошибке, например, «oms-connection-not-found».
        /// </summary>
        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Описание ошибки, например, «Идентификатор соединения для указанного УОТ не найден».
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
