using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Документ "Отгрузка".
    /// </summary>
    public class Shipment : ITrueApiDocument
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        DocumentType ITrueApiDocument.DocumentType => Spoleto.TrueApi.DocumentType.LP_SHIP_GOODS;

        /// <summary>
        /// Номер первичного документа
        /// </summary>
        [JsonPropertyName("document_num")]
        [Required]
        public string DocumentNum { get; set; }

        /// <summary>
        /// Дата первичного документа
        /// </summary>
        /// <remarks>
        /// Задаётся в формате yyyy-MM-dd
        /// </remarks>
        [JsonPropertyName("document_date")]
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [Required]
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// Дата передачи маркированных товаров
        /// </summary>
        /// <remarks>
        /// Задаётся в формате yyyy-MM-dd
        /// </remarks>
        [JsonPropertyName("transfer_date")]
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [Required]
        public DateTime? TransferDate { get; set; }

        /// <summary>
        /// ИНН получателя
        /// </summary>
        [JsonPropertyName("receiver_inn")]
        [Required]
        public string ReceiverInn { get; set; }

        /// <summary>
        /// ИНН отправителя
        /// </summary>
        [JsonPropertyName("sender_inn")]
        public string SenderInn { get; set; }

        /// <summary>
        /// Признак отгрузки неучастнику
        /// </summary>
        /// <remarks>
        /// Возможные значения:
        /// true – отгрузка неучастнику;
        /// false – отгрузка УОТ
        /// </remarks>
        [JsonPropertyName("to_not_participant")]
        public bool ToNotParticipant { get; set; }

        /// <summary>
        /// Код типа отгрузки
        /// </summary>
        [JsonPropertyName("turnover_type")]
        [Required]
        public TurnoverType TurnoverType { get; set; }

        /// <summary>
        /// Список сведений о товарах
        /// </summary>
        [JsonPropertyName("products")]
        [Required]
        public List<ShipmentItem> Products { get; set; }
    }
}
