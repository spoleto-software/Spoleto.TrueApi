using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Изображение товара для создания фида.
    /// </summary>
    public class NkImageCreatingModel
    {
        /// <summary>
        /// Тип изображения
        /// </summary>
        [JsonConverter(typeof(NkPhotoTypeJsonConverter))]
        [JsonPropertyName("photo_type")]
        [Required]
        public NkPhotoType PhotoType { get; set; }

        /// <summary>
        /// Url либо массив Url
        /// </summary>
        /// <remarks>
        /// Массив url указывается при photo_type=3ds, в этом случае надо передать все url через запятую
        /// </remarks>
        [JsonConverter(typeof(NkPhotoUrlJsonConverter))]
        [JsonPropertyName("photo_url")]
        [Required]
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Идентификатор заданного типа
        /// </summary>
        /// <remarks>
        /// Значение gtin/barcode. Используется для связи фотографий с идентификаторами товара.
        /// </remarks>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Тип идентификатора
        /// </summary>
        /// <remarks>
        /// Обязательно при наличии identifier.
        /// </remarks>
        [JsonPropertyName("identifier_type")]
        public NkIdentifierType? IdentifierType { get; set; }

        /// <summary>
        /// Идентификатор торговой сети
        /// </summary>
        /// <remarks>
        /// Необязательно при identifier_type = gtin, обязательно при других типах
        /// </remarks>
        [JsonPropertyName("identifier_party_id")]
        public int? IdentifierPartyId { get; set; }
    }
}
