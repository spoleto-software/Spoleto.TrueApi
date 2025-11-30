using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Изображение товара для получения полной информации о товаре.
    /// </summary>
    public class NkProductImageModel
    {
        /// <summary>
        /// Тип изображения
        /// </summary>
        [JsonConverter(typeof(NkProductPhotoTypeJsonConverter))]
        [JsonPropertyName("photo_type")]
        [Required]
        public NkProductPhotoType PhotoType { get; set; }

        /// <summary>
        /// Дата создания фотографии
        /// </summary>
        /// <remarks>
        /// UTC yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("photo_date")]
        [Required]
        public DateTime PhotoDate { get; set; }

        /// <summary>
        /// Ссылка на med(medium) размер фотографии
        /// </summary>
        [JsonPropertyName("photo_url")]
        [Required]
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Штрих-код или артикул товара, для которого сделана фотография
        /// </summary>
        [JsonPropertyName("barcode")]
        public string Barcode { get; set; }
    }
}
