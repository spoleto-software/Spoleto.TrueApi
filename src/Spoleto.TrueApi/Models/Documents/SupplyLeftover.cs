using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Документ ввода остатков в оборот
    /// </summary>
    public class SupplyLeftover : ITrueApiDocument
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        DocumentType ITrueApiDocument.DocumentType => DocumentType.LP_INTRODUCE_OST;

        /// <summary>
        /// ИНН участника оборота товаров
        /// </summary>
        [JsonPropertyName("trade_participant_inn")]
        [Required]
        public string TradeParticipantInn { get; set; }

        /// <summary>
        /// Список товаров на ввод в оборот
        /// </summary>
        [JsonPropertyName("products_list")]
        [Required]
        public List<SupplyLeftoverItem> ProductsList { get; set; }
    }
}
