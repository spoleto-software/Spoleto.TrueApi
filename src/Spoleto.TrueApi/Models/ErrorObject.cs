using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Объект описания ошибок обработки документа
    /// </summary>
    public record ErrorObject
    {
        /// <summary>
        /// Код ошибки
        /// </summary>
        [JsonPropertyName("code")]
        [Required]
        public int Code { get; set; }

        /// <summary>
        /// Текстовое описание кодов ошибок
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Содержит текстовое значение кода ошибки
        /// </summary>
        [JsonPropertyName("error")]
        public ErrorEntry Error { get; set; }

        /// <summary>
        /// Содержит внутри себя детали ошибки
        /// </summary>
        [JsonPropertyName("data")]
        public ErrorData Data { get; set; }
    }
}
