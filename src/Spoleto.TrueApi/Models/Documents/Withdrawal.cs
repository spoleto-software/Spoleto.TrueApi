using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Документ "Вывод из оборота".
    /// </summary>
    public class Withdrawal : ITrueApiDocument
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        DocumentType ITrueApiDocument.DocumentType => Spoleto.TrueApi.DocumentType.LK_RECEIPT;

        /// <summary>
        /// Причина вывода из оборота
        /// </summary>
        [JsonPropertyName("action")]
        [Required]
        public WithdrawReason Action { get; set; }

        /// <summary>
        /// Дата вывода из оборота
        /// </summary>
        /// <remarks>
        /// Задаётся в формате yyyy-MM-dd
        /// </remarks>
        [JsonPropertyName("action_date")]
        [Required]
        public DateTime? ActionDate { get; set; }

        /// <summary>
        /// Дата первичного документа
        /// </summary>
        /// <remarks>
        /// Задаётся в формате yyyy-MM-dd
        /// </remarks>
        [JsonPropertyName("document_date")]
        [Required]
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// Номер первичного документа
        /// </summary>
        [JsonPropertyName("document_number")]
        [Required]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Вид первичного документа
        /// </summary>
        /// <remarks>
        /// Вид первичного документа зависит от параметра "action"
        /// </remarks>
        [JsonPropertyName("document_type")]
        [Required]
        public PrimaryDocumentType DocumentType { get; set; }

        /// <summary>
        /// ИНН УОТ
        /// </summary>
        [JsonPropertyName("inn")]
        [Required]
        public string Inn { get; set; }

        /// <summary>
        /// Контрольно-кассовая техника
        /// </summary>
        /// <remarks>
        /// Номер кассы
        /// </remarks>
        [JsonPropertyName("kkt_number")]
        public string KktNumber { get; set; }

        /// <summary>
        /// Приложенный PDF файл в Base64
        /// </summary>
        [JsonPropertyName("pdfFile")]
        public string PdfFile { get; set; }

        /// <summary>
        /// Наименование первичного документа
        /// </summary>
        /// <remarks>
        /// Обязательно, если в поле «Вид первичного документа» указано прочее
        /// </remarks>
        [JsonPropertyName("primary_document_custom_name")]
        public string PrimaryDocumentCustomName { get; set; }

        /// <summary>
        /// Товар в заявке
        /// </summary>
        [JsonPropertyName("products")]
        [Required]
        public List<WithdrawalItem> Products { get; set; }
    }
}
