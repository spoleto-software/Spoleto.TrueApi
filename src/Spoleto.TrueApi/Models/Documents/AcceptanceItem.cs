using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Товар для приемки
    /// </summary>
    public class AcceptanceItem
    {
        /// <summary>
        /// Признак того, что товар принят или не принят
        /// </summary>
        [JsonPropertyName("accepted")]
        [Required]
        public bool Accepted { get; set; }

        /// <summary>
        /// Уникальный КИ/КИН
        /// </summary>
        /// <remarks>
        /// Обязательный, если не указан "uitu_code".
        /// </remarks>
        [JsonPropertyName("uit_code")]
        public string UitCode { get; set; }

        /// <summary>
        /// Уникальный идентификатор транспортной упаковки
        /// </summary>
        /// <remarks>
        /// Обязательный, если не указан "uit_code"
        /// </remarks>
        [JsonPropertyName("uitu_code")]
        public string UituCode { get; set; }

        ///// <summary>
        ///// Цена за единицу
        ///// </summary>
        ///// <remarks>
        ///// Стоимость указывается в копейках с учётом НДС
        ///// </remarks>
        //[JsonPropertyName("product_cost")]
        //public int? ProductCost { get; set; }

        ///// <summary>
        ///// Сумма НДС
        ///// </summary>
        ///// <remarks>
        ///// Сумма НДС указывается в копейках
        ///// </remarks>
        //[JsonPropertyName("product_tax")]
        //public int? ProductTax { get; set; }

        /// <summary>
        /// Описание товара
        /// </summary>
        [JsonPropertyName("product_description")]
        public string ProductDescription { get; set; }

        /// <summary>
        /// КИ в агрегате
        /// </summary>
        /// <remarks>
        /// Информация о вложенных КИ для частичной приёмки
        /// </remarks>
        [JsonPropertyName("children")]
        public List<string> Children { get; set; }

        /// <summary>
        /// Количество дочерних КИ
        /// </summary>
        [JsonPropertyName("count_children")]
        public int? CountChildren { get; set; }
    }
}
