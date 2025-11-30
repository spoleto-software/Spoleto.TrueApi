using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Сведения о товаре.
    /// </summary>
    public class SupplyLeftoverItem
    {
        /// <summary>
        /// Уникальный идентификатор товара КИ
        /// </summary>
        [JsonPropertyName("ki")]
        public string Ki { get; set; }


    }
}
