using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Местонахождение товара.
    /// </summary>
    public class NkProductLocationAddressModel
    {
        /// <summary>
        /// Название страны
        /// </summary>
        /// <remarks>
        /// ISO 3166-2
        /// </remarks>
        [JsonPropertyName("country")]
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Название города
        /// </summary>
        [JsonPropertyName("city")]
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Название улицы, дом
        /// </summary>
        [JsonPropertyName("street")]
        [Required]
        public string Street { get; set; }

        /// <summary>
        /// Координаты
        /// </summary>
        [JsonPropertyName("location")]
        [Required]
        public NkProductLocationCoordinatesModel Location { get; set; }

        public override string ToString() => $"{Country} {City} {Street}";
    }
}
