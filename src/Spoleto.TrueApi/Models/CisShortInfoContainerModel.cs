using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Контейнер с краткой информацией о кодах идентификации (КИ).
    /// </summary>
    public class CisShortInfoContainerModel
    {
        /// <summary>
        /// Массив с информацией о КИ.
        /// </summary>
        [JsonPropertyName("result")]
        [Required]
        public CisShortInfoModel Result { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Код ошибки
        /// </summary>
        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; }

        public override string ToString()
            => String.IsNullOrEmpty(ErrorMessage)
            ? Result?.ToString()
            : $"{Result?.Cis} - {ErrorMessage}";
    }
}
