using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Содержимое документа, полученное из True Api сервиса.
    /// </summary>
    /// <remarks>
    /// Общая информация, включая статус обработки документа, о запрашиваемом документе (в одном запросе указывается один документ).
    /// </remarks>
    /// <typeparam name="T">Тип документа.</typeparam>
    public class DocumentInfoReportModel : ITrueApiDocument
    {
        /// <summary>
        /// ID документа
        /// </summary>
        /// <remarks>
        /// Указывается полное наименование файла для УД
        /// </remarks>
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// Дата и время документа
        /// </summary>
        [JsonPropertyName("docDate")]
        public DateTime DocDate { get; set; }

        /// <summary>
        /// Дата и время получения документа
        /// </summary>
        [JsonPropertyName("receivedAt")]
        public DateTime ReceivedAt { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        /// <remarks>
        /// См.Справочник "Типы документов".
        /// </remarks>
        [JsonPropertyName("type")]
        public DocumentType DocumentType { get; set; }

        /// <summary>
        /// Статус обработки документа
        /// </summary>
        /// <remarks>
        /// См.Справочник "Статусы документов".
        /// </remarks>
        [JsonPropertyName("status")]
        public DocumentStatus DocumentStatus { get; set; }

        /// <summary>
        /// ИНН отправителя документа
        /// </summary>
        [JsonPropertyName("senderInn")]
        public string SenderInn { get; set; }

        /// <summary>
        /// Наименование отправителя документа
        /// </summary>
        [JsonPropertyName("senderName")]
        public string SenderName { get; set; }

        /// <summary>
        /// ИНН получателя документа
        /// </summary>
        [JsonPropertyName("receiverInn")]
        public string ReceiverInn { get; set; }

        /// <summary>
        /// Наименование получателя документа
        /// </summary>
        [JsonPropertyName("receiverName")]
        public string ReceiverName { get; set; }

        /// <summary>
        /// Номер счёта- фактуры, УКД
        /// </summary>
        [JsonPropertyName("invoiceNumber")]
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Дата счёта- фактуры, УКД
        /// </summary>
        [JsonPropertyName("invoiceDate")]
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// ID документа отгрузки или приёмки
        /// </summary>
        [JsonPropertyName("relatedDocId")]
        public string RelatedDocId { get; set; }

        /// <summary>
        /// Ошибки. Параметр возвращается при наличии ошибки.
        /// </summary>
        /// <remarks>
        /// Для УД ошибки не возвращаются. Используется для вывода ошибок по документа ГИС прямой подачи сведений
        /// </remarks>
        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }

        /// <summary>
        /// Список общих ошибок обработки документа
        /// </summary>
        /// <remarks>
        /// Значение параметра возвращается при наличии ошибки всех типов документов прямой подачи и УПД, УПДи, УКД, УКДи
        /// </remarks>
        [JsonPropertyName("commonErrors")]
        public List<CommonError> CommonErrors { get; set; }

        /// <summary>
        /// Вид товарооборота
        /// </summary>
        [JsonPropertyName("turnoverType")]
        public string TurnoverType { get; set; }

        /// <summary>
        /// Товарная группа
        /// </summary>
        [JsonPropertyName("productGroup")]
        public List<string> ProductGroup { get; set; }

        /// <summary>
        /// ID товарной группы
        /// </summary>
        [JsonPropertyName("productGroupId")]
        public List<int> ProductGroupId { get; set; }
    }

    public class DocumentInfoReportModel<T> : DocumentInfoReportModel where T : ITrueApiDocument
    {
        /// <summary>
        /// Тело документа
        /// </summary>
        [JsonPropertyName("body")]
        public T Body { get; set; }
    }
}
