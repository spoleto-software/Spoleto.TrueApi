using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Координаты местонахождения товара.
    /// </summary>
    public class NkProductLocationCoordinatesModel
    {
        /// <summary>
        /// Географическая широта
        /// </summary>
        [JsonPropertyName("lat")]
        [Required]
        public string Latitude { get; set; }

        /// <summary>
        /// Географическая долгота
        /// </summary>
        [JsonPropertyName("lon")]
        [Required]
        public string Longitude { get; set; }

        public override string ToString() => $"{Latitude}; {Longitude}";
    }
}
