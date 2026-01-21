using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Auth.Models
{
    /// <summary>
    /// Ответ сервиса с ошибкой.
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// Код ошибки
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        [JsonPropertyName("error_message")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Описание ошибки
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        public override string ToString() => ErrorMessage;
    }
}
