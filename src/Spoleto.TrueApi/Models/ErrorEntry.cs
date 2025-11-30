using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Содержит текстовое значение кода ошибки
    /// </summary>
    public record ErrorEntry
    {
        /// <summary>
        /// Значение из столбца "Тестовое описание кода ошибки"
        /// </summary>
        [JsonPropertyName("CisNotExists")]
        public string[] CisNotExists { get; set; }

        /// <summary>
        /// Текстовое описание ошибки
        /// </summary>
        [JsonPropertyName("details")]
        public string Details { get; set; }
    }
}
