using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Товар для ввода в оборот. Трансграничная торговля (Мех)
    /// </summary>
    public class SupplyImportCrossborderFurItem : SupplyImportCrossborderItemBase
    {
        /// <summary>
        /// Цена за единицу
        /// </summary>
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        [JsonPropertyName("vat_value")]
        public decimal? VatValue { get; set; }
    }
}
