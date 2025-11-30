using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Квитанция результата обработки документов (УПД, УПД(и), УКД, УКД(и), УПД(отгрузка продукции) и предложения об аннулировании УД) в ГИС МТ.
    /// </summary>
    public record ProcessingResultModel
    {
        /// <summary>
        /// Идентификатор результирующей квитанции (цепочки операций)
        /// Значение соответствует значению из атрибута /Квитанция/ГИСМТ/ЛС/@ИдТК транспортной квитанции
        /// </summary>
        [JsonPropertyName("resultDocId")]
        [Required]
        public string ResultDocId { get; set; }

        /// <summary>
        /// Дата формирования результирующей технологической квитанции
        /// </summary>
        [JsonPropertyName("resultDocDate")]
        [Required]
        public long ResultDocDate { get; set; }

        /// <summary>
        /// Идентификатор логического сообщения в ГИС МТ
        /// Значение соответствует значению атрибута /Квитанция/ГИСМТ/ЛС/@ИдЛС Внутренней транспортной квитанции
        /// </summary>
        [JsonPropertyName("sourceDocId")]
        [Required]
        public string SourceDocId { get; set; }

        /// <summary>
        /// Дата получения входящего логического сообщения
        /// Дата формате UnixDataTime (в миллисекундах)
        /// </summary>
        [JsonPropertyName("sourceDocDate")]
        [Required]
        public long SourceDocDate { get; set; }

        /// <summary>
        /// Результат обработки исходного документа (УПД, УКД)
        /// Возможные значения: SUCCESS, FAILED, IN_PROGRESS
        /// </summary>
        [JsonPropertyName("state")]
        [Required]
        public ProcessingResultState State { get; set; }

        /// <summary>
        /// Код результата выполнения обработки
        /// Возможные значения:
        /// 0 – SUCCESS
        /// 1 – FAILED
        /// 2 – IN_PROGRESS
        /// </summary>
        [JsonPropertyName("code")]
        [Required]
        public int Code { get; set; }

        /// <summary>
        /// Текстовое описание результата обработки
        /// </summary>
        [JsonPropertyName("description")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Список операций, связанных с обработкой исходного документа
        /// </summary>
        [JsonPropertyName("operations")]
        [Required]
        public Operation[] Operations { get; set; }
    }
}
