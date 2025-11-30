using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Идентификатор информации о товаре.
    /// </summary>
    public class NkProductIdentifierModel
    {
        /// <summary>
        /// Штрих-код или локальный идентификатор
        /// </summary>
        [JsonPropertyName("value")]
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Тип идентификатора
        /// </summary>
        [JsonPropertyName("type")]
        [Required]
        public NkIdentifierType Type { get; set; }

        /// <summary>
        /// Идентификатор торговой сети
        /// </summary>
        /// <remarks>
        /// Возвращается только при условии, что параметр type имеет значение barcode
        /// </remarks>
        [JsonPropertyName("party_id")]
        public string PartyId { get; set; }

        /// <summary>
        /// Количество товаров в в упаковке
        /// </summary>
        /// <remarks>
        /// Значение по умолчанию = 1.
        /// </remarks>
        [JsonPropertyName("multiplier")]
        [Required]
        public int Multiplier { get; set; }

        /// <summary>
        /// Тип упаковки (уровень упаковки)
        /// </summary>
        [JsonConverter(typeof(NkPackTypeJsonConverter))]
        [JsonPropertyName("level")]
        [Required]
        public NkPackType Level { get; set; }

        public override string ToString() => $"{Type}: {Value}";
    }
}
