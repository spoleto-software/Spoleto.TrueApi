using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Код товара (GTIN).
    /// </summary>
    public class NkGtinResultModel
    {
        /// <summary>
        /// Код товара, который был сгенерированы
        /// </summary>
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        public override string ToString() => Gtin;
    }
}
