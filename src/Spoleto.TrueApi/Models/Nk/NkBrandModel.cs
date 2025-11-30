using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Торговые марки.
    /// </summary>
    public class NkBrandModel : INkObject
    {
        /// <summary>
        /// Идентификатор бренда
        /// </summary>
        [JsonPropertyName("brand_id")]
        public int BrandId { get; set; }

        /// <summary>
        /// Наименование бренда
        /// </summary>
        [JsonPropertyName("brand_name")]
        public string BrandName { get; set; }

        public override string ToString() => BrandName;
    }
}
