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
    public class DocumentInfoReportModel<T> : ITrueApiDocument
    {
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
        /// Ошибки. Параметр возвращается при наличии ошибки.
        /// </summary>
        /// <remarks>
        /// Для УД ошибки не возвращаются. Используется для вывода ошибок по документа ГИС прямой подачи сведений
        /// </remarks>
        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }


    }
}
