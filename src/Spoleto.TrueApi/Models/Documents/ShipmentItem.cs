using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Товар для отгрузки
    /// </summary>
    public class ShipmentItem
    {
        /// <summary>
        /// Уникальный КИ/КИН
        /// </summary>
        /// <remarks>
        /// Обязательный, если не указан "uitu_code"
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

        /// <summary>
        /// Наименования продукта
        /// </summary>
        [JsonPropertyName("product_description")]
        [Required]
        public string ProductDescription { get; set; }

        ///// <summary>
        ///// Цена за единицу
        ///// </summary>
        ///// <remarks>
        ///// Стоимость указывается в копейках с учётом НДС
        ///// </remarks>
        //[JsonPropertyName("product_cost")]
        ////[JsonIgnore]
        //public int? ProductCost { get; set; }

        ///// <summary>
        ///// Сумма НДС
        ///// </summary>
        ///// <remarks>
        ///// Сумма НДС указывается в копейках
        ///// </remarks>
        //[JsonPropertyName("product_tax")]
        ////[JsonIgnore]
        //public int? ProductTax { get; set; }
    }
}
