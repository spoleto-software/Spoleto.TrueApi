using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Перемаркировка товара
    /// </summary>
    public class ProductRemarkModel
    {
        /// <summary>
        /// Текущий КИ товара
        /// </summary>
        [JsonPropertyName("curr")]
        public string Curr { get; set; }

        /// <summary>
        /// Дата перемаркиовки.
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z (обязательный для ТГ "Молочная продукция")
        /// </remarks>
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// предыдущие КИ товара
        /// </summary>
        [JsonPropertyName("prev")]
        public string Prev { get; set; }
    }
}
