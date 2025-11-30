using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Цены на товар по торговым сетям данного аккаунта.
    /// </summary>
    public class NkProductLocationModel
    {
        /// <summary>
        /// Идентификатор торговой сети
        /// </summary>
        [JsonPropertyName("party_id")]
        [Required]
        public int PartyId { get; set; }

        /// <summary>
        /// Местонахождение
        /// </summary>
        [JsonPropertyName("address")]
        [Required]
        public NkProductLocationAddressModel Address { get; set; }

        public override string ToString() => Address?.ToString();
    }
}
