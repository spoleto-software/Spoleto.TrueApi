using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Идентификатор для создания фида
    /// </summary>
    public class NkIdentifierCreatingModel
    {
        /// <summary>
        /// Тип идентификатора
        /// </summary>
        [JsonPropertyName("type")]
        [Required]
        public NkIdentifierType Type { get; set; }

        /// <summary>
        /// Значение идентификатора
        /// </summary>
        [JsonPropertyName("value")]
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Тип упаковки (уровень упаковки)
        /// </summary>
        [JsonConverter(typeof(NkPackTypeJsonConverter))]
        [JsonPropertyName("level")]
        [Required]
        public NkPackType Level { get; set; }

        /// <summary>
        /// Идентификатор атрибута (значение - 13763)
        /// </summary>
        /// <remarks>
        /// Обязателен для всех типов упаковки, кроме trade-unit
        /// </remarks>
        [JsonPropertyName("attr_id")]
        public int? AttributeId { get; set; }

        /// <summary>
        /// Значение атрибута: GTIN вложения в упаковку. Указывается GTIN упаковки предыдущего уровня
        /// </summary>
        /// <remarks>
        /// Указывается код упаковки предыдущего уровня.
        /// Обязателен для всех типов упаковки, кроме trade-unit
        /// </remarks>
        [JsonPropertyName("attr_value")]
        public int? AttributeValue { get; set; }

        /// <summary>
        /// Количество в упаковке
        /// </summary>
        /// <remarks>
        /// Значение по умолчанию = 1.
        /// </remarks>
        [JsonPropertyName("multiplier")]
        [Required]
        public int Multiplier { get; set; } = 1;

        /// <summary>
        /// Идентификатор торговой сети
        /// </summary>
        /// <remarks>
        /// Необязательно при типе gtin, обязательно при других типах
        /// </remarks>
        [JsonPropertyName("party_id")]
        public string PartyId { get; set; }

        /// <summary>
        /// Тип измерений
        /// </summary>
        [JsonPropertyName("unit")]
        public NkUnitType Unit { get; set; }
    }
}
