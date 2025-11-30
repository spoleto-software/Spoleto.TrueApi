using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Контейнер с подробной общедоступной информацией о кодах идентификации (КИ).
    /// </summary>
    public class CisPublicInfoContainerModel
    {
        /// <summary>
        /// Подробная общедоступная информация о кодах идентификации (КИ).
        /// </summary>
        /// <remarks>
        /// При наличии КИ в ГИС МТ
        /// </remarks>
        [JsonPropertyName("cisInfo")]
        public CisPublicInfoModel CisInfo { get; set; }

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
            ? CisInfo?.ToString()
            : $"{CisInfo?.Cis} - {ErrorMessage}";
    }
}
