using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Документ "Перемаркировка"
    /// </summary>
    public class Remarking : ITrueApiDocument
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        DocumentType ITrueApiDocument.DocumentType => Spoleto.TrueApi.DocumentType.LK_REMARK;

        /// <summary>
        /// ИНН УОТ
        /// </summary>
        /// <remarks>
        /// string (10, 12)
        /// </remarks>
        [JsonPropertyName("participant_inn")]
        [Required]
        public string ParticipantInn { get; set; }

        /// <summary>
        /// Дата повторной маркировки
        /// </summary>
        /// <remarks>
        /// Задаётся в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [JsonPropertyName("remarking_date")]
        [Required]
        public DateTime? RemarkingDate { get; set; }

        /// <summary>
        /// Код причины повторной маркировки
        /// </summary>
        [JsonPropertyName("remarking_cause")]
        [Required]
        public RemarkingCause RemarkingCause { get; set; }

        /// <summary>
        /// Список сведений о товарах
        /// </summary>
        [JsonPropertyName("products")]
        [Required]
        public List<RemarkingItem> Products { get; set; }
    }
}
