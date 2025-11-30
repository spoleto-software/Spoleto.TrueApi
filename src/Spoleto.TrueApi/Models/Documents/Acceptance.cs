using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Документ "Приемка".
    /// </summary>
    public class Acceptance : ITrueApiDocument
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        DocumentType ITrueApiDocument.DocumentType => DocumentType.LP_ACCEPT_GOODS;

        /// <summary>
        /// Номер первичного документа
        /// </summary>
        [JsonPropertyName("document_number")]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        /// <remarks>
        /// Значение параметра должно = "ACCEPTANCE"
        /// </remarks>
        [JsonPropertyName("request_type")]
        [Required]
        public string RequestType { get; set; } = "ACCEPTANCE";

        /// <summary>
        /// Отклонить все
        /// </summary>
        /// <remarks>
        /// Если значение true, то список товаров можно не указывать: отклоняются все коды, перечисленные в документе отгрузки.
        /// Поля, обязательные для заполнения: "release_order_number", "trade_sender_inn", "trade_recipient_inn".
        /// Если параметр "reject_all" и "accept_all" = false, то анализируется параметр "accept" для каждого кода в документе "Приёмка".
        /// Чтобы принять
        /// </remarks>
        [JsonPropertyName("reject_all")]
        public bool? RejectAll { get; set; }

        /// <summary>
        /// Дата первичного документа
        /// </summary>
        /// <remarks>
        /// Задаётся в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [JsonPropertyName("document_date")]
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// Принять все
        /// </summary>
        /// <remarks>
        /// Если значение true, то список товаров можно не указывать: принимаются все коды, перечисленные в документе отгрузки.
        /// Поля, обязательные для заполнения: "release_order_number", "trade_sender_inn", "trade_recipient_inn".
        /// </remarks>
        [JsonPropertyName("accept_all")]
        public bool? AcceptAll { get; set; }

        /// <summary>
        /// Наименование отправителя
        /// </summary>
        [JsonPropertyName("trade_sender_name")]
        public string TradeSenderName { get; set; }

        /// <summary>
        /// Дата передачи товара
        /// </summary>
        /// <remarks>
        /// Задаётся в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [JsonPropertyName("transfer_date")]
        public DateTime? TransferDate { get; set; }

        /// <summary>
        /// Дата приемки товара
        /// </summary>
        /// <remarks>
        /// Задаётся в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [JsonPropertyName("acceptance_date")]
        [Required]
        public DateTime? AcceptanceDate { get; set; }

        /// <summary>
        /// ИНН отправителя
        /// </summary>
        [JsonPropertyName("trade_sender_inn")]
        [Required]
        public string TradeSenderInn { get; set; }

        /// <summary>
        /// ИНН получателя
        /// </summary>
        [JsonPropertyName("trade_recipient_inn")]
        [Required]
        public string TradeRecipientInn { get; set; }

        /// <summary>
        /// Код типа отгрузки
        /// </summary>
        [JsonPropertyName("turnover_type")]
        public TurnoverType? TurnoverType { get; set; }

        /// <summary>
        /// Номер отгрузки
        /// </summary>
        [JsonPropertyName("release_order_number")]
        [Required]
        public string ReleaseOrderNumber { get; set; }

        /// <summary>
        /// Уникальный идентификатор экспортёра в национальной системе учета налогоплательщиков
        /// </summary>
        /// <remarks>
        /// Параметр указывается только для приёмки документа "Отгрузка при трансграничной торговле",
        /// и при этом release_method = "CROSSBORDER"
        /// </remarks>
        [JsonPropertyName("exporter_taxpayer_id")]
        public string ExporterTaxpayerId { get; set; }

        /// <summary>
        /// Наименование экспортёра
        /// </summary>
        /// <remarks>
        /// Параметр указывается только для приёмки документа "Отгрузка при трансграничной торговле",
        /// и при этом release_method = "CROSSBORDER"
        /// </remarks>
        [JsonPropertyName("exporter_name")]
        public string ExporterName { get; set; }

        /// <summary>
        /// Код способа ввода в оборот
        /// </summary>
        /// <remarks>
        /// Параметр указывается только для приёмки документа "Отгрузка при трансграничной торговле",
        /// и при этом его значение должно быть = "CROSSBORDER"
        /// </remarks>
        [JsonPropertyName("release_method")]
        public string ReleaseMethod { get; set; }

        /// <summary>
        /// Список сведений о товарах
        /// </summary>
        [JsonPropertyName("products")]
        [Required]
        public List<AcceptanceItem> Products { get; set; }
    }
}
